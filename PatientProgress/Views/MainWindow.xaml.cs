using Microsoft.Extensions.DependencyInjection;
using PatientProgress.Data;
using PatientProgress.Models;
using PatientProgress.Services;
using PatientProgress.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PatientProgress.Views
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _services;

        public MainWindow(MainWindowViewModel viewModel, IServiceProvider services)
        {
            InitializeComponent();
            DataContext = viewModel;
            _services = services;
        }

        private void OnPatientDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((sender as ListView)?.SelectedItem is Patient selectedPatient)
            {
                var db = _services.GetRequiredService<ApplicationDbContext>();
                var imageService = _services.GetRequiredService<PatientImageService>();
                var vm = new PatientImagesViewModel(selectedPatient, imageService, db);

                var window = new PatientImagesWindow(vm);
                window.Owner = this;
                window.ShowDialog();
            }
        }
    }
}