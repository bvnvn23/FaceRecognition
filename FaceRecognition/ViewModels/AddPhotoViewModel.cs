using System;
using ReactiveUI;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using FaceRecognition.Helpers;
using MsBox.Avalonia;
using FaceRecognition.Services;


namespace FaceRecognition.ViewModels;

public class AddPhotoViewModel : ViewModelBase
{
    private readonly PhotoService _photoService;
    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
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
    
    private Window _currentWindow;
    public ICommand UploadPhotoCommand { get; }
    public ICommand BrowseFileCommand { get; }
    public ICommand ReturnCommand { get; }
    

    public AddPhotoViewModel(Window currentWindow, PhotoService photoService)
    {
        _currentWindow = currentWindow;
        _photoService = photoService;
        
        UploadPhotoCommand = new RelayCommand(UploadPhoto);
        BrowseFileCommand = new AsyncRelayCommand(BrowseFile);
        ReturnCommand = new RelayCommand(Return, () => true);
    }

    private async void UploadPhoto()
    {
        if (UploadedPhoto != null && Name != null)
        {
            var photo = ImageConversionHelper.ImageToByteArray(_uploadedPhoto);
            _photoService.AddPhotoAsync(photo, Name);
            var box = MessageBoxManager.GetMessageBoxStandard("Success", "Photo uploaded successfully");
            await box.ShowAsync();
             Return();
        }
        else
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error", "No photo or name provided");

            await box.ShowAsync();
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

                if (!string.IsNullOrEmpty(path))
                {
                    UploadedPhoto = new Avalonia.Media.Imaging.Bitmap(path);
                }
            }
        }
    }

    private void Return()
    {
       ReturnToMainMenuHelper.ReturnToMainMenu();
       _currentWindow.Close();
    }
}