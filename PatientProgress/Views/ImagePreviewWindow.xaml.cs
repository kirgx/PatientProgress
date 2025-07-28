using System.Windows;
using System.Windows.Media.Imaging;

namespace PatientProgress.Views
{
    public partial class ImagePreviewWindow : Window
    {
        public ImagePreviewWindow(string imagePath)
        {
            InitializeComponent();

            PreviewImage.Source = new BitmapImage(
                new Uri(System.IO.Path.Combine(AppContext.BaseDirectory, imagePath)));
        }
    }
}
