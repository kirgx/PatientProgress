using PatientProgress.Data;
using PatientProgress.Models;
using PatientProgress.Services;
using Serilog;
using System.IO;
using System.Windows;

namespace PatientProgress.Services;

public class PatientImageService
{
    private readonly ApplicationDbContext _db;
    private readonly ImageProcessingService _imageProcessor;

    public PatientImageService(ApplicationDbContext db, ImageProcessingService imageProcessor)
    {
        _db = db;
        _imageProcessor = imageProcessor;
    }

    public async Task UploadImageAsync(string filePath, Patient patient)
    {
        if (!filePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
            !filePath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) &&
            !filePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show("Допустимы только изображения JPEG/PNG.");
            return;
        }

        try
        {
            var baseDir = AppContext.BaseDirectory;
            var patientFolder = Path.Combine(baseDir, "Images", patient.Id.ToString());
            Directory.CreateDirectory(patientFolder);

            var targetPath = Path.Combine(patientFolder, Path.GetFileName(filePath));

            await _imageProcessor.ResizeAndSaveAsync(filePath, targetPath);

            var image = new PatientImage
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id,
                UploadDate = DateTime.Now,
                FilePath = targetPath
            };

            _db.PatientImages.Add(image);
            await _db.SaveChangesAsync();

            Log.Information("Загружено изображение для пациента {FullName}", patient.FullName);
            MessageBox.Show("Изображение успешно загружено!");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Ошибка загрузки изображения для пациента {FullName}", patient.FullName);
            MessageBox.Show("Произошла ошибка при загрузке изображения.");
        }
    }
}
