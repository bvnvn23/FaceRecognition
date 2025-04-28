using FaceRecognition.Data;
using FaceRecognition.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<List<ImageData>> GetAllPhotosAsync()
    {
        return await _dbContext.Images.ToListAsync();
    }

    public async Task DeletePhotoAsync(int id)
    {
        var image = await _dbContext.Images.FindAsync(id);
        if (image != null)
        {
            _dbContext.Images.Remove(image);
            await _dbContext.SaveChangesAsync();
        }
    }
    
}
