using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using NeoIsisJob.Repos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using NeoIsisJob.Commands;
using System.Security.Claims;


namespace NeoIsisJob.ViewModels.Classes
{
    public class ClassesViewModel: INotifyPropertyChanged
    {
        private readonly ClassService _classService;
        private readonly ClassTypeService _classTypeService;
        private readonly PersonalTrainerService _personalTrainerService;
        private ObservableCollection<ClassModel> _classes;
        private ObservableCollection<ClassTypeModel> _classTypes;
        private ObservableCollection<PersonalTrainerModel> _personalTrainers;
        private ClassTypeModel _selectedClassType;

        public ICommand DeleteClassCommand { get; }
        public ICommand UpdateClassCommand { get; }
        public ICommand CloseEditPopupCommand { get; }
        public ClassesViewModel()
        {
            this._classService = new ClassService();
            this._classTypeService = new ClassTypeService();
            this._personalTrainerService = new PersonalTrainerService();
            Classes = new ObservableCollection<ClassModel>();
            ClassTypes = new ObservableCollection<ClassTypeModel>();
            PersonalTrainers = new ObservableCollection<PersonalTrainerModel>();

            LoadClasses();
            LoadClassTypes();
        }

        public ObservableCollection<ClassModel> Classes
        {
            get => _classes;
            set
            {
                _classes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ClassTypeModel> ClassTypes
        {
            get => _classTypes;
            set
            {
                _classTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PersonalTrainerModel> PersonalTrainers
        {
            get => _personalTrainers;
            set
            {
                _personalTrainers = value;
                OnPropertyChanged();
            }
        }

        private void LoadClasses()
        {
            Classes.Clear();

            var trainersDict = _personalTrainerService.GetAllPersonalTrainers()
                              .ToDictionary(t => t.Id);  // Convert to Dictionary for fast lookup

            foreach (var classItem in _classService.GetAllClasses())
            {
                // Assign Personal Trainer to Class
                if (trainersDict.TryGetValue(classItem.PersonalTrainerId, out var trainer))
                {
                    classItem.PersonalTrainer = trainer;
                }

                Classes.Add(classItem);
            }
        }


        private void LoadClassTypes()
        {
            ClassTypes.Clear();
            foreach (var classType in this._classTypeService.GetAllClassTypes())
            {
                ClassTypes.Add(classType);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
