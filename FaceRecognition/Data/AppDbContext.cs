using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FaceRecognition.Models;
using System;
using System.IO;

namespace FaceRecognition.Data;

public class AppDbContext : DbContext
{
    public DbSet<ImageData> Images { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var databasePath = Path.Combine(AppContext.BaseDirectory, "images.db");
        optionsBuilder.UseSqlite($"Data Source={databasePath}");

        Console.WriteLine("Baza danych podłączona pod ścieżkę:");
        Console.WriteLine(databasePath);
    }
}