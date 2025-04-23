using Microsoft.EntityFrameworkCore;
using FaceRecognition.Models;

namespace FaceRecognition.Data;

public class AppDbContext : DbContext
{
   public DbSet<ImageData> Images { get; set; }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlite("Data Source=images.db");
   }
}