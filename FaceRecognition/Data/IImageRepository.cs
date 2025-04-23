using System.Collections.Generic;
using System.Threading.Tasks;
using FaceRecognition.Models;

namespace FaceRecognition.Data;

public interface IImageRepository
{
    Task<IEnumerable<ImageData>> GetAllImages();
    Task<ImageData?> GetImageById(int id);
    Task AddImage(ImageData imageData);
    Task DeleteImage(ImageData imageData);
}