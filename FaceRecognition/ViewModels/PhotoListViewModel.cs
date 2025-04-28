using System.Collections.ObjectModel;
using ReactiveUI;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using FaceRecognition.Helpers;
using FaceRecognition.Services;
using FaceRecognition.Models;
using MsBox.Avalonia;

namespace FaceRecognition.ViewModels;

public class PhotoListViewModel
{
    private readonly PhotoService _photoService;
    private PersonViewModel? _selectedPerson;
    public PersonViewModel? SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            _selectedPerson = value;
            (DeleteCommand as RelayCommand)?.NotifyCanExecuteChanged();
        }
    }
    private Window _currentWindow;
    public ICommand DeleteCommand { get; set; }
    public ICommand ReturnCommand { get; set; }
    public ObservableCollection<PersonViewModel> People { get; set; } = new();

    public PhotoListViewModel(Window currentWindow, PhotoService photoService)
    {
        _currentWindow = currentWindow;
        _photoService = photoService;
        
        DeleteCommand = new RelayCommand(RemoveSelectedPerson, () => SelectedPerson != null);
        ReturnCommand = new RelayCommand(() => ReturnToMainMenuHelper.ReturnButton(_currentWindow), () => true);
        
        GetPhotosFromDatabase();
    }

    public async void GetPhotosFromDatabase()
    {
        var photos = await _photoService.GetAllPhotosAsync();

        foreach (var photo in photos)
        {
            People.Add(new PersonViewModel()
            {
                Id = photo.Id,
                Name = photo.Name,
                Image = ImageConversionHelper.ByteArrayToBitmap(photo.Data),
            });
        }
    }

    public async void RemoveSelectedPerson()
    {
        if (SelectedPerson != null)
        {
            var confirmBox = MessageBoxManager
                .GetMessageBoxStandard("Confirm", "Are you sure you want to delete this person?",
                                       MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await confirmBox.ShowAsync();

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                await _photoService.DeletePhotoAsync(SelectedPerson.Id);
                People.Remove(SelectedPerson);
            }
        }
        else
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error!", "You must select a person to delete");
            await box.ShowAsync();
        }
    }
    
}