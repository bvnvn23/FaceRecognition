using System.IO;
using Avalonia.Media.Imaging;

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
}