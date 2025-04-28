using Avalonia.Controls;
using FaceRecognition.Data;
using FaceRecognition.Services;
using FaceRecognition.ViewModels;

namespace FaceRecognition.Views;

public partial class PhotoListWindow : Window
{
    public PhotoListWindow()
    {
        InitializeComponent();

        DataContext = new PhotoListViewModel(this, new PhotoService(new AppDbContext()));
        
        Width = 450;
        Height = 450;
        CanResize = false;
        SizeToContent = SizeToContent.Manual;
    }
    
}