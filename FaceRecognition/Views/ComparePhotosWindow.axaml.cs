using FaceRecognition.ViewModels;
using FaceRecognition.Data;
using FaceRecognition.Services;
using Avalonia.Controls;

namespace FaceRecognition.Views;

public partial class ComparePhotosWindow : Window
{
    public ComparePhotosWindow()
    {
        DataContext = new ComparePhotosViewModel(this, new PhotoService(new AppDbContext()));
        
        InitializeComponent();
        
        Width = 450;
        Height = 375;
        CanResize = false;
        SizeToContent = SizeToContent.Manual;
    }
}