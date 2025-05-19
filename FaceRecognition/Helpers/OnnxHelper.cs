using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FaceRecognition.Helpers
{
    public static class OnnxHelper
    {
        private static readonly InferenceSession _session;

        static OnnxHelper()
        {
            try
            {
                var modelPath = Path.Combine(AppContext.BaseDirectory, "OnnxModel", "arcfaceresnet100-8.onnx");
        
                if (!File.Exists(modelPath))
                    throw new FileNotFoundException("Model ONNX not found at path: " + modelPath);

                _session = new InferenceSession(modelPath);
                Console.WriteLine("[INFO] ONNX model loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to initialize ONNX model: {ex.Message}");
                Console.WriteLine("[HINT] Make sure the file 'arcfaceresnet100-8.onnx' exists in the 'OnnxModel' directory inside the build output path.");
                throw;
            }
        }

        public static float[] GetEmbedding(float[] inputData)
        {
            var inputTensor = new DenseTensor<float>(inputData, new[] { 1, 3, 112, 112 });
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("data", inputTensor)
            };

            using var results = _session.Run(inputs);
            return results.First().AsEnumerable<float>().ToArray();
        }

        public static float CosineSimilarity(float[] vec1, float[] vec2)
        {
            float dot = 0f, normA = 0f, normB = 0f;

            for (int i = 0; i < vec1.Length; i++)
            {
                dot += vec1[i] * vec2[i];
                normA += vec1[i] * vec1[i];
                normB += vec2[i] * vec2[i];
            }

            return dot / (MathF.Sqrt(normA) * MathF.Sqrt(normB));
        }
    }
}