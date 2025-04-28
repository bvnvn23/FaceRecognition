namespace FaceRecognition.Models;
using Avalonia.Media.Imaging;

public class PersonViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Bitmap? Image { get; set; }
}