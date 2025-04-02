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
        private ObservableCollection<ClassModel> _classes;
        private ObservableCollection<ClassTypeModel> _classTypes;
        private ClassTypeModel _selectedClassType;

        public ICommand DeleteClassCommand { get; }
        public ICommand UpdateClassCommand { get; }
        public ICommand CloseEditPopupCommand { get; }
        public ClassesViewModel()
        {
            this._classService = new ClassService();
            this._classTypeService = new ClassTypeService();
            Classes = new ObservableCollection<ClassModel>();
            ClassTypes = new ObservableCollection<ClassTypeModel>();

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

        private void LoadClasses()
        {
            Classes.Clear();

            foreach (var classes in this._classService.GetAllClasses())
            {
                Classes.Add(classes);
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
