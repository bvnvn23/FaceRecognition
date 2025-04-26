using FaceRecognition.ViewModels;
using FaceRecognition.Data;
using FaceRecognition.Services;
using Avalonia.Controls;

namespace FaceRecognition.Views;

public partial class AddPhotoWindow : Window
{
    public AddPhotoWindow()
    {
        InitializeComponent();
        DataContext = new AddPhotoViewModel(this, new PhotoService(new AppDbContext()));
        
        Width = 450;
        Height = 450;
        CanResize = false;
        SizeToContent = SizeToContent.Manual;

    }
}