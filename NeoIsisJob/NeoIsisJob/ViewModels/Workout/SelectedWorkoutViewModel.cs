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
    public class SelectedWorkoutViewModel : INotifyPropertyChanged
    {
        private readonly WorkoutService _workoutService;
        //for getting the exercise by id
        private readonly ExerciseService _exerciseService;
        private readonly CompleteWorkoutService _completeWorkoutService;
        private WorkoutModel _selectedWorkout;
        private ObservableCollection<CompleteWorkoutModel> _completeWorkouts;

        public WorkoutModel SelectedWorkout
        {
            get => _selectedWorkout;
            set 
            { 
                _selectedWorkout = value;
                //signal that the property has changed
                OnPropertyChanged();

                //update the collection
                IList<CompleteWorkoutModel> complWorkouts = FilledCompleteWorkoutsWithExercies(this._completeWorkoutService.GetCompleteWorkoutsByWorkoutId(this._selectedWorkout.Id));
                CompleteWorkouts = new ObservableCollection<CompleteWorkoutModel>(complWorkouts);
            }
        }

        public ObservableCollection<CompleteWorkoutModel> CompleteWorkouts
        {
            get => _completeWorkouts;
            set
            {
                _completeWorkouts = value;
                OnPropertyChanged();
            }
        }

        public SelectedWorkoutViewModel()
        {
            this._workoutService = new WorkoutService();
            this._exerciseService = new ExerciseService();
            this._completeWorkoutService = new CompleteWorkoutService();
            this._completeWorkouts = new ObservableCollection<CompleteWorkoutModel>();
        }

        public IList<CompleteWorkoutModel> FilledCompleteWorkoutsWithExercies(IList<CompleteWorkoutModel> complWorkouts)
        {
            foreach (CompleteWorkoutModel complWorkout in complWorkouts)
            {
                complWorkout.Exercise = this._exerciseService.GetExerciseById(complWorkout.ExerciseId);
            }

            return complWorkouts;
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
