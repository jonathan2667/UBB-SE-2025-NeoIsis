using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.ViewModels.Workout
{
    public class CreateWorkoutViewModel : INotifyPropertyChanged
    {
        private readonly WorkoutTypeService _workoutTypeService;
        private readonly ExerciseService _exerciseService;
        private readonly MuscleGroupService _muscleGroupService;
        private ObservableCollection<WorkoutTypeModel> _workoutTypes;
        private ObservableCollection<ExercisesModel> _exercises;
        private WorkoutTypeModel _selectedWorkoutType;

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

        public CreateWorkoutViewModel()
        {
            this._workoutTypeService = new WorkoutTypeService();
            this._exerciseService = new ExerciseService();
            this._muscleGroupService = new MuscleGroupService();  
            this.WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();
            this.Exercises = new ObservableCollection<ExercisesModel>();
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

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        //gets triggered every time a property changes
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
