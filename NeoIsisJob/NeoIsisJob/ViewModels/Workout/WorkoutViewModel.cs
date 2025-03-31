using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using NeoIsisJob.Commands;
using System.Collections.Generic;
using System.Linq;

namespace NeoIsisJob.ViewModels.Workout
{
    //INotifyPropertyChanged notifies the client that a property value has changed
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        private readonly WorkoutService _workoutService;
        private readonly WorkoutTypeService _workoutTypeService;
        private ObservableCollection<WorkoutModel> _workouts;
        private ObservableCollection<WorkoutTypeModel> _workoutTypes;
        private WorkoutTypeModel _selectedWorkoutType;

        //Workouts property for the list -> when set it, signal that it changed
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
                //when setting, call the function too to update the displayed list
                _selectedWorkoutType = value;
                OnPropertyChanged();
                //apply the filter when changed
                ApplyWorkoutFilter();
            }
        }

        public ICommand LoadWorkoutsCommand { get; }

        public ICommand LoadWorkoutTypesCommand { get; }

        public WorkoutViewModel()
        {
            this._workoutService = new WorkoutService();
            this._workoutTypeService = new WorkoutTypeService();
            Workouts = new ObservableCollection<WorkoutModel>();
            WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();

            // Command to load workouts --- FOR NOW IT IS COMMENTED!
            //LoadWorkoutsCommand = new RelayCommand(LoadWorkouts);

            // Load workouts when viewmodel is created
            LoadWorkouts();
            LoadWorkoutTypes();
        }

        private void LoadWorkouts()
        {
            Workouts.Clear();

            //for debugging
            //var allWorkouts = _workoutService.GetAllWorkouts();

            //System.Diagnostics.Debug.WriteLine($"Workout count: {allWorkouts.Count}");

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

            //if a checkbox is selected
            if(SelectedWorkoutType != null)
            {
                //filter the list
                foreach(WorkoutModel workout in allWorkouts.Where(w => w.WorkoutTypeId == SelectedWorkoutType.Id))
                {
                    Workouts.Add(workout);
                }
            }
            //if not
            else
            {
                foreach(WorkoutModel workout in allWorkouts)
                {
                    Workouts.Add(workout);
                }
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        //gets triggered every time a property changes
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
