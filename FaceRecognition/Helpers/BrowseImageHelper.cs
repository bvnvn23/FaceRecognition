using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace FaceRecognition.Helpers;

public class BrowseImageHelper
{
    public static async Task<string?> BrowseImageFileAsync(Window parentWindow)
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter
                {
                    Name = "Obrazy",
                    Extensions = { "png", "jpg", "jpeg" }
                }
            }
        };

        var result = await dialog.ShowAsync(parentWindow);
        return result?.FirstOrDefault();
    }
}