using System.Windows;
using System.Windows.Input;
using PatientProgress.Data;
using PatientProgress.Models;
using PatientProgress.Services;
using PatientProgress.ViewModels;

namespace PatientProgress.Views;

public partial class PatientImagesWindow : Window
{
    public PatientImagesWindow(PatientImagesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private async void OnDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (DataContext is PatientImagesViewModel vm)
                await vm.HandleDroppedFiles(files);
        }
    }

    private void OnDragOver(object sender, DragEventArgs e)
    {
        e.Handled = true;
    }

    private void OnImageClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is PatientImage image)
        {
            var previewWindow = new ImagePreviewWindow(image.FilePath);
            previewWindow.Owner = this;
            previewWindow.ShowDialog();
        }
    }
}