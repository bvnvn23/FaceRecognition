using Avalonia.Controls;

namespace FaceRecognition.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Width = 450;
        Height = 450;
        CanResize = false;
        SizeToContent = SizeToContent.Manual;
    }
}