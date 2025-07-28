using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PatientProgress.Models;
using PatientProgress.Data;
using PatientProgress.Commands;
using PatientProgress.ViewModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace PatientProgress.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ApplicationDbContext _dbContext;

    public MainWindowViewModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        AddPatientCommand = new RelayCommand(AddPatient, CanAddPatient);

        LoadPatients();
    }

    public ICommand AddPatientCommand { get; }

    public ObservableCollection<Patient> Patients { get; } = new();

    private string _newPatientName;
    public string NewPatientName
    {
        get => _newPatientName;
        set
        {
            _newPatientName = value;
            OnPropertyChanged();
            ((RelayCommand)AddPatientCommand).RaiseCanExecuteChanged();
        }
    }

    private DateTime? _newPatientBirthday = DateTime.Today;
    public DateTime? NewPatientBirthday
    {
        get => _newPatientBirthday;
        set
        {
            _newPatientBirthday = value;
            OnPropertyChanged();
            ((RelayCommand)AddPatientCommand).RaiseCanExecuteChanged();
        }
    }

    private void LoadPatients()
    {
        try
        {
            var patients = _dbContext.Patients.AsNoTracking().ToList();
            Patients.Clear();
            foreach (var patient in patients)
                Patients.Add(patient);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Ошибка при загрузке списка пациентов");
        }
    }

    private bool CanAddPatient()
    {
        return !string.IsNullOrWhiteSpace(NewPatientName) && NewPatientBirthday.HasValue;
    }

    private void AddPatient()
    {
        try
        {
            var newPatient = new Patient
            {
                Id = Guid.NewGuid(),
                FullName = NewPatientName.Trim(),
                DateOfBirth = NewPatientBirthday.Value
            };

            _dbContext.Patients.Add(newPatient);
            _dbContext.SaveChanges();
            Patients.Add(newPatient);

            Log.Information("Пациент добавлен: {FullName}", newPatient.FullName);

            NewPatientName = string.Empty;
            NewPatientBirthday = DateTime.Today;
            OnPropertyChanged(nameof(NewPatientName));
            OnPropertyChanged(nameof(NewPatientBirthday));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Ошибка при добавлении пациента");
        }
    }
}
