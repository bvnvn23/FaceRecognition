using Avalonia.Media.Imaging;
using FaceRecognition.Helpers;

namespace FaceRecognition.Services;

public class FaceEmbeddingService
{
    public float[] GetEmbeddingFromBitmap(Bitmap bitmap)
    {
        var input = ImageConversionHelper.BitmapToFloatArray(bitmap);
        return OnnxHelper.GetEmbedding(input);
    }
}