using Microsoft.UI.Xaml.Controls;
using NeoIsisJob.Commands;
using NeoIsisJob.Models;
using NeoIsisJob.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeoIsisJob.ViewModels.Workout
{
    public class CreateWorkoutViewModel : INotifyPropertyChanged
    {
        private readonly Frame _frame;

        private readonly WorkoutTypeService _workoutTypeService;
        private readonly ExerciseService _exerciseService;
        private readonly MuscleGroupService _muscleGroupService;
        private readonly WorkoutService _workoutService;
        private readonly CompleteWorkoutService _completeWorkoutService;
        private ObservableCollection<WorkoutTypeModel> _workoutTypes;
        private ObservableCollection<ExercisesModel> _exercises;

        //for the add functionality
        private String _selectedWorkoutName;
        private WorkoutTypeModel _selectedWorkoutType;
        private ObservableCollection<ExercisesModel> _selectedExercises;
        private int _selectedNumberOfSets;
        private int _selectedNumberOfRepsPerSet;
        

        public ObservableCollection<WorkoutTypeModel> WorkoutTypes 
        {
            get {  return _workoutTypes; }
            set
            {
                _workoutTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ExercisesModel> Exercises
        {
            get { return _exercises; }
            set
            {
                _exercises = value;
                OnPropertyChanged();
            }
        }

        //property for the current selected workout type
        public WorkoutTypeModel SelectedWorkoutType
        {
            get { return _selectedWorkoutType; }
            set
            {
                _selectedWorkoutType = value;
                OnPropertyChanged();
            }
        }

        public String SelectedWorkoutName
        {
            get { return _selectedWorkoutName; }
            set
            {
                _selectedWorkoutName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ExercisesModel> SelectedExercises
        {
            get { return _selectedExercises; }
            set
            {
                _selectedExercises = value;
                OnPropertyChanged();
            }
        }

        public int SelectedNumberOfSets
        {
            get { return _selectedNumberOfSets; }
            set
            {
                _selectedNumberOfSets = value;
                OnPropertyChanged();
            }
        }

        public int SelectedNumberOfRepsPerSet
        {
            get { return _selectedNumberOfRepsPerSet; }
            set
            {
                _selectedNumberOfRepsPerSet = value;
                OnPropertyChanged();
            }
        }

        //command for add
        public ICommand CreateWorkoutAndCompleteWorkoutsCommand { get; }

        //command for cancel
        public ICommand CancelCommand { get; }

        public CreateWorkoutViewModel(Frame frame)
        {
            //pass the frame to the viewModel
            this._frame = frame;

            this._workoutTypeService = new WorkoutTypeService();
            this._exerciseService = new ExerciseService();
            this._muscleGroupService = new MuscleGroupService(); 
            this._workoutService = new WorkoutService();
            this._completeWorkoutService = new CompleteWorkoutService();
            this.WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();
            this.Exercises = new ObservableCollection<ExercisesModel>();
            this.SelectedExercises = new ObservableCollection<ExercisesModel>();

            //initialize the commands
            CreateWorkoutAndCompleteWorkoutsCommand = new RelayCommand(CreateWorkoutAndCompleteWorkouts);
            CancelCommand = new RelayCommand(Cancel);

            LoadWorkoutTypes();
            LoadExercises();
        }

        public void LoadWorkoutTypes()
        {
            WorkoutTypes.Clear();

            foreach(WorkoutTypeModel workoutType in this._workoutTypeService.GetAllWorkoutTypes())
            {
                this.WorkoutTypes.Add(workoutType); 
            }
        }

        public void LoadExercises()
        {
            Exercises.Clear();

            foreach (ExercisesModel exercise in this._exerciseService.GetAllExercises())
            {
                //add the corresponding MuscleGroup object to every one
                exercise.MuscleGroup = this._muscleGroupService.GetMuscleGroupById(exercise.MuscleGroupId);
                this.Exercises.Add(exercise);
            }
        }

        //function that will serve a command bound to the save button
        public void CreateWorkoutAndCompleteWorkouts()
        {
            //save the workout and then save all entries in CompleteWorkouts

            //here add the workout
            this._workoutService.InsertWorkout(SelectedWorkoutName, SelectedWorkoutType.Id);
            int selectedWorkoutId = this._workoutService.GetWorkoutByName(SelectedWorkoutName).Id;

            //here add all the entries in CompleteWorkouts
            foreach(ExercisesModel exercise in SelectedExercises)
            {
                this._completeWorkoutService.InsertCompleteWorkout(selectedWorkoutId, exercise.Id, SelectedNumberOfSets, SelectedNumberOfRepsPerSet);
            }

            //now go to back to the prev page
            if(this._frame.CanGoBack)
                this._frame.GoBack();
        }

        public void Cancel()
        {
            if (this._frame.CanGoBack)
                this._frame.GoBack();
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
