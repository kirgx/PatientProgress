using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PatientProgress.Commands;
using PatientProgress.Data;
using PatientProgress.Models;
using PatientProgress.Services;
using Serilog;

namespace PatientProgress.ViewModels;

public class PatientImagesViewModel : ViewModelBase
{
    private readonly PatientImageService _imageService;
    private readonly ApplicationDbContext _dbContext;

    public ObservableCollection<PatientImage> Images { get; set; } = new();

    public Patient SelectedPatient { get; }

    public ICommand UploadImageCommand { get; }

    public ObservableCollection<string> ImagePaths { get; } = new();

    public PatientImagesViewModel(
        Patient selectedPatient,
        PatientImageService imageService,
        ApplicationDbContext dbContext)
    {
        SelectedPatient = selectedPatient;
        _imageService = imageService;
        _dbContext = dbContext;

        UploadImageCommand = new RelayCommand(UploadFromFilePicker);

        LoadImages();
    }

    private void LoadImages()
    {
        var images = _dbContext.PatientImages
            .Where(i => i.PatientId == SelectedPatient.Id)
            .OrderByDescending(i => i.UploadDate)
            .ToList();

        Images.Clear();
        foreach (var img in images)
            Images.Add(img);
    }

    private void UploadFromFilePicker()
    {
        var dlg = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Изображения|*.jpg;*.jpeg;*.png",
            Multiselect = true
        };

        if (dlg.ShowDialog() == true)
        {
            foreach (var file in dlg.FileNames)
            {
                _ = _imageService.UploadImageAsync(file, SelectedPatient)
                    .ContinueWith(_ => Application.Current.Dispatcher.Invoke(LoadImages));
            }
        }
    }

    public async Task HandleDroppedFiles(string[] filePaths)
    {
        foreach (var file in filePaths)
        {
            await _imageService.UploadImageAsync(file, SelectedPatient);
        }

        LoadImages();
    }
}
