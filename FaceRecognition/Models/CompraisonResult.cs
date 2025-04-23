using System;
namespace FaceRecognition.Models;

public class CompraisonResult
{
    public ImageData? ImageData { get; set; }
    public double Score { get; set; }
    public bool IsSimilar { get; set; }
    string? MatchedLabel { get; set; }
    string? StrategyUsed { get; set; }
    DateTime ComparedAt { get; set; }
}
