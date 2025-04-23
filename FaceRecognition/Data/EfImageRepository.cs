using System.Collections.Generic;
using System.Threading.Tasks;
using FaceRecognition.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceRecognition.Data;

public class EfImageRepository : IImageRepository
{
    private readonly AppDbContext _context;

    public EfImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ImageData>> GetAllImages()
    {
        return await _context.Images.ToListAsync();
    }

    public async Task<ImageData?> GetImageById(int id)
    {
        return await _context.Images.FindAsync(id);
    }

    public async Task AddImage(ImageData imageData)
    {
        await _context.Images.AddAsync(imageData);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteImage(ImageData imageData)
    {
        var image = await _context.Images.FindAsync(imageData.Id);
        if (image != null)
        {
            _context.Images.Remove(image);
        }
        await _context.SaveChangesAsync();
    }
}