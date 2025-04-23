namespace FaceRecognition.Models;

public class ImageData
{
    public int Id { get; set; }
    public byte[] Data { get; set; }
    public string? Label { get; set; }
}