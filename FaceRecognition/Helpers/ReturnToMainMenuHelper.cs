using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace FaceRecognition.Helpers;

public class ReturnToMainMenuHelper
{
    public static void ReturnToMainMenu()
    {
        var mainWindow = new Views.MainWindow();
        mainWindow.DataContext = new ViewModels.MainWindowViewModel();

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.MainWindow = mainWindow;
        }

        mainWindow.Show();
    }
    
    public static void ReturnButton(Window _currentWindow)
    {
        ReturnToMainMenu();
        _currentWindow.Close();
    }
}