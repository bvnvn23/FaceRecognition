using System;
using System.IO;
using Avalonia.Media.Imaging;
using System.Runtime.InteropServices;
using Avalonia.Platform;

namespace FaceRecognition.Helpers;

public class ImageConversionHelper
{
    public static byte[] ImageToByteArray(Bitmap bitmap)
    {
        using (var stream = new MemoryStream())
        {
            bitmap.Save(stream);
            return stream.ToArray();
        }
    }

    public static Bitmap ByteArrayToBitmap(byte[] bytes)
    {
        using (var stream = new MemoryStream(bytes))
        {
            return new Bitmap(stream);
        }
    }

    public static float[] BitmapToFloatArray(Bitmap bitmap)
    {
        try
        {
            var resized = bitmap.CreateScaledBitmap(new Avalonia.PixelSize(112, 112));

            int width = 112;
            int height = 112;
            int stride = width * 4;
            var buffer = new byte[height * stride];

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();

            resized.CopyPixels(
                new Avalonia.PixelRect(0, 0, width, height),
                ptr,
                buffer.Length,
                stride
            );

            handle.Free();

            float[] result = new float[1 * 3 * 112 * 112];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixelIndex = y * stride + x * 4;
                    if (pixelIndex + 2 >= buffer.Length)
                    {
                        Console.WriteLine($"Index out of bounds at x={x}, y={y}, pixelIndex={pixelIndex}, bufferLength={buffer.Length}");
                        continue;
                    }
                    byte b = buffer[pixelIndex + 0];
                    byte g = buffer[pixelIndex + 1];
                    byte r = buffer[pixelIndex + 2];

                    int index = y * width + x;

                    result[0 * width * height + index] = (r - 127.5f) / 128f;
                    result[1 * width * height + index] = (g - 127.5f) / 128f;
                    result[2 * width * height + index] = (b - 127.5f) / 128f;
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine("BitmapToFloatArray crashed: " + ex);
            throw;
        }
    }
    
}