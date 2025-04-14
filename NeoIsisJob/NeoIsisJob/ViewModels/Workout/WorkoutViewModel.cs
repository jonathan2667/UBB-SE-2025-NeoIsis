using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using NeoIsisJob.Commands;

namespace NeoIsisJob.ViewModels.Workout
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        // Add WorkoutService, WorkoutTypeService, and CompleteWorkoutService as fields
        private readonly WorkoutService _workoutService;
        private readonly WorkoutTypeService _workoutTypeService;
        private readonly CompleteWorkoutService _completeWorkoutService;
        private ObservableCollection<WorkoutModel> _workouts;
        private ObservableCollection<WorkoutTypeModel> _workoutTypes;
        private WorkoutTypeModel _selectedWorkoutType;

        // Add SelectedWorkoutViewModel as a property
        public SelectedWorkoutViewModel SelectedWorkoutViewModel { get; }

        // Add commands for deleting, updating, and closing the edit popup
        public ICommand DeleteWorkoutCommand { get; }
        public ICommand UpdateWorkoutCommand { get; }
        public ICommand CloseEditPopupCommand { get; }

        public WorkoutViewModel()
        {
            this._workoutService = new WorkoutService();
            this._workoutTypeService = new WorkoutTypeService();
            this._completeWorkoutService = new CompleteWorkoutService();
            Workouts = new ObservableCollection<WorkoutModel>();
            WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();

            // Initialize SelectedWorkoutViewModel
            SelectedWorkoutViewModel = new SelectedWorkoutViewModel();

            // Initialize commands
            DeleteWorkoutCommand = new RelayCommand<int>(DeleteWorkout);
            UpdateWorkoutCommand = new RelayCommand<string>(UpdateWorkout);
            CloseEditPopupCommand = new RelayCommand(CloseEditPopup);

            // Load workouts and workout types
            LoadWorkouts();
            LoadWorkoutTypes();
        }

        // Add properties for Workouts, WorkoutTypes, and SelectedWorkoutType
        public ObservableCollection<WorkoutModel> Workouts
        {
            get => _workouts;
            set
            {
                _workouts = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WorkoutTypeModel> WorkoutTypes
        {
            get => _workoutTypes;
            set
            {
                _workoutTypes = value;
                OnPropertyChanged();
            }
        }

        public WorkoutTypeModel SelectedWorkoutType
        {
            get => _selectedWorkoutType;
            set
            {
                _selectedWorkoutType = value;
                OnPropertyChanged();
                ApplyWorkoutFilter();
            }
        }

        // Expose SelectedWorkout from SelectedWorkoutViewModel
        public WorkoutModel SelectedWorkout
        {
            get => SelectedWorkoutViewModel.SelectedWorkout;
            set => SelectedWorkoutViewModel.SelectedWorkout = value;
        }

        private bool _isEditPopupOpen;
        public bool IsEditPopupOpen
        {
            get => _isEditPopupOpen;
            set
            {
                _isEditPopupOpen = value;
                OnPropertyChanged();
            }
        }

        private void LoadWorkouts()
        {
            Workouts.Clear();

            foreach (var workout in this._workoutService.GetAllWorkouts())
            {
                Workouts.Add(workout);
            }
        }

        private void LoadWorkoutTypes()
        {
            WorkoutTypes.Clear();
            foreach (var workoutType in this._workoutTypeService.GetAllWorkoutTypes())
            {
                WorkoutTypes.Add(workoutType);
            }
        }

        private void ApplyWorkoutFilter()
        {
            Workouts.Clear();
            IList<WorkoutModel> allWorkouts = this._workoutService.GetAllWorkouts();

            if (SelectedWorkoutType != null)
            {
                foreach (WorkoutModel workout in allWorkouts.Where(w => w.WorkoutTypeId == SelectedWorkoutType.Id))
                {
                    Workouts.Add(workout);
                }
            }
            else
            {
                foreach (WorkoutModel workout in allWorkouts)
                {
                    Workouts.Add(workout);
                }
            }
        }

        public void ApplyWorkoutTypeFilter(WorkoutTypeModel selectedType, bool isChecked)
        {
            if (isChecked)
            {
                SelectedWorkoutType = selectedType;
            }
            else
            {
                SelectedWorkoutType = null;
            }
        }

        public void DeleteWorkout(int workoutId)
        {
            // Delete the selected workout and its complete workouts
            this._completeWorkoutService.DeleteCompleteWorkoutsByWorkoutId(workoutId);
            this._workoutService.DeleteWorkout(workoutId);

            // Loading workouts again
            LoadWorkouts();
        }

        public void UpdateWorkout(string newName)
        {
            // Update the selected workout's name
            if (SelectedWorkout != null && !string.IsNullOrWhiteSpace(newName))
            {
                SelectedWorkout.Name = newName;
                _workoutService.UpdateWorkout(SelectedWorkout);
                LoadWorkouts();
                IsEditPopupOpen = false;
            }
        }

        private void CloseEditPopup()
        {
            // Close the edit popup
            IsEditPopupOpen = false;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}