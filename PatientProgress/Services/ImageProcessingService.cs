using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace PatientProgress.Services
{
    public class ImageProcessingService
    {
        public async Task ResizeAndSaveAsync(string sourcePath, string targetPath)
        {
            try
            {
                using var image = await Image.LoadAsync(sourcePath);
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(1920, 1080),
                    Mode = ResizeMode.Max
                }));

                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                await image.SaveAsync(targetPath);

                Log.Information("Изображение сохранено: {Path}", targetPath);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка обработки изображения: {Path}", sourcePath);
                throw;
            }
        }
    }
}
