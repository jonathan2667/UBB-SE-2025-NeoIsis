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
        private readonly CompleteWorkoutService _completeWorkoutService;
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

        public ICommand DeleteWorkoutCommand { get; }

        public WorkoutViewModel()
        {
            this._workoutService = new WorkoutService();
            this._workoutTypeService = new WorkoutTypeService();
            this._completeWorkoutService = new CompleteWorkoutService();
            Workouts = new ObservableCollection<WorkoutModel>();
            WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();

            // Commands
            //LoadWorkoutsCommand = new RelayCommand(LoadWorkouts);
            DeleteWorkoutCommand = new RelayCommand<int>(DeleteWorkout);


            // Load workouts when viewmodel is created
            LoadWorkouts();
            LoadWorkoutTypes();
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

            //if a checkbox is selected
            if (SelectedWorkoutType != null)
            {
                //filter the list
                foreach (WorkoutModel workout in allWorkouts.Where(w => w.WorkoutTypeId == SelectedWorkoutType.Id))
                {
                    Workouts.Add(workout);
                }
            }
            //if not
            else
            {
                foreach (WorkoutModel workout in allWorkouts)
                {
                    Workouts.Add(workout);
                }
            }
        }

        public void DeleteWorkout(int wid)
        {
            //delete the entries from complete workouts then delete the workout
            this._completeWorkoutService.DeleteCompleteWorkoutsByWid(wid);
            this._workoutService.DeleteWorkout(wid);

            //after deletion, load the workouts again
            LoadWorkouts();
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