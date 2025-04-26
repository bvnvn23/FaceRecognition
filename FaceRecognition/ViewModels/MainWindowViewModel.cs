using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FaceRecognition.Views;

namespace FaceRecognition.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ICommand ComparePhotoCommand { get; }
    public ICommand AddPhotoCommand { get; }
    public ICommand DeletePhotoCommand { get; }
    public ICommand ViewDatabaseCommand { get; }
    public ICommand ExitCommand { get; }

    public MainWindowViewModel()
    {
        ComparePhotoCommand = new RelayCommand(ComparePhoto);
        AddPhotoCommand = new RelayCommand(AddPhoto);
        DeletePhotoCommand = new RelayCommand(DeletePhoto);
        ViewDatabaseCommand = new RelayCommand(ViewDatabase);
        ExitCommand = new RelayCommand(Exit);
    }

    private void ComparePhoto()
    {
        //ComparePhoto
    }
    private void AddPhoto()
    {
        var addPhotoWindow = new Views.AddPhotoWindow();
        addPhotoWindow.Show();
        Exit();
    }

    private void DeletePhoto()
    {
        //DeletePhotoView
    }

    private void ViewDatabase()
    {
        //VewDatabaseView
    }
    private void Exit()
    {
        var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        mainWindow?.Close();
    }
}