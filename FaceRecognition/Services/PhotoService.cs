using FaceRecognition.Data;
using FaceRecognition.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FaceRecognition.Services;

public class PhotoService
{
    private readonly AppDbContext _dbContext;

    public PhotoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddPhotoAsync(byte[] data, string name)
    {
        var imageData = new ImageData
        {
            Name = name,
            Data = data,
        };
        
        _dbContext.Images.Add(imageData);
        await _dbContext.SaveChangesAsync();
    }
}