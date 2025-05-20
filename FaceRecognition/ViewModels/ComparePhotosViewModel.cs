using System.IO;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using FaceRecognition.Helpers;
using FaceRecognition.Services;
using MsBox.Avalonia;

namespace FaceRecognition.ViewModels;

public class ComparePhotosViewModel : ViewModelBase
{
    private Bitmap _uploadedPhoto = null!;

    public Bitmap UploadedPhoto
    {
        get => _uploadedPhoto;
        set
        {
            if (_uploadedPhoto != value)
            {
                _uploadedPhoto = value;
                OnPropertyChanged();
            }
        }
    }

    private float[] _uploadedEmbedding = null!;
    public float[] UploadedEmbedding
    {
        get => _uploadedEmbedding;
        private set
        {
            _uploadedEmbedding = value;
            OnPropertyChanged();
        }
    }

    private Bitmap _bestMatchPhoto = null!;
    public Bitmap BestMatchPhoto
    {
        get => _bestMatchPhoto;
        set
        {
            if (_bestMatchPhoto != value)
            {
                _bestMatchPhoto = value;
                OnPropertyChanged();
            }
        }
    }

    private Window _currentWindow;
    private PhotoService _photoService;
    public ICommand ComparePhotosCommand { get; }
    public ICommand BrowseFileCommand { get; }
    public ICommand ReturnCommand { get; }


    public ComparePhotosViewModel(Window currentWindow, PhotoService photoService)
    {
        
        _currentWindow = currentWindow;
        _photoService = photoService;
        
        ComparePhotosCommand = new RelayCommand(ComparePhotos);
        BrowseFileCommand = new AsyncRelayCommand(BrowseFile);
        ReturnCommand = new RelayCommand(() =>
        {
            if (_currentWindow is not null)
                ReturnToMainMenuHelper.ReturnButton(_currentWindow);
        });
    }

    private async void ComparePhotos()
    {
        Console.WriteLine($"[DEBUG] UploadedPhoto is {(UploadedPhoto == null ? "null" : "not null")}");
        if (UploadedPhoto == null || UploadedEmbedding == null)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "You need to select an image or embedding image.");
            await box.ShowAsync();
            return;
        }

        try
        {
            var allPhotos = await _photoService.GetAllPhotosAsync();
            var embeddingService = new FaceEmbeddingService();
            
            
            float highestSimilarity = -1f;
            string? bestMatchName = null;
            Bitmap? bestMatchBitmap = null;

            foreach (var photo in allPhotos)
            {
                var referenceBitmap = ImageConversionHelper.ByteArrayToBitmap(photo.Data);
                var referenceEmbedding = embeddingService.GetEmbeddingFromBitmap(referenceBitmap);

                float similarity = OnnxHelper.CosineSimilarity(UploadedEmbedding, referenceEmbedding);

                if (similarity > highestSimilarity)
                {
                    highestSimilarity = similarity;
                    bestMatchName = photo.Name;
                    bestMatchBitmap = referenceBitmap;
                }
            }

            if (highestSimilarity > 0.95f)
            {
                BestMatchPhoto = bestMatchBitmap!;
                var box = MessageBoxManager.GetMessageBoxStandard("Result", $"Best result: {bestMatchName} (similarity: {highestSimilarity:F2})");
                await box.ShowAsync();
            }
            else
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Result", "Didn't match result. Similarity too low.");
                await box.ShowAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when comparing photos: {ex.Message}");
        }
    }

    public async Task BrowseFile()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = desktop.MainWindow;
            if (window != null)
            {
                string? path = await BrowseImageHelper.BrowseImageFileAsync(window);
                try
                {
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        UploadedPhoto = new Avalonia.Media.Imaging.Bitmap(path);
                        Console.WriteLine("[DEBUG] UploadedPhoto assigned successfully");
                        var embeddingService = new FaceEmbeddingService();
                        UploadedEmbedding = embeddingService.GetEmbeddingFromBitmap(UploadedPhoto);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error when loading an image: {ex.Message}");
                }
            }
        }
    }
}