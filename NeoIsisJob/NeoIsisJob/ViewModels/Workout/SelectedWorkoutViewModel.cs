using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using NeoIsisJob.Servs.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NeoIsisJob.ViewModels.Workout
{
    public class SelectedWorkoutViewModel : INotifyPropertyChanged
    {
        private readonly IWorkoutService _workoutService;
        //for getting the exercise by id
        private readonly IExerciseService _exerciseService;
        private readonly ICompleteWorkoutService _completeWorkoutService;
        private WorkoutModel _selectedWorkout;
        private ObservableCollection<CompleteWorkoutModel> _completeWorkouts;

        public WorkoutModel SelectedWorkout
        {
            get => _selectedWorkout;
            set 
            { 
                _selectedWorkout = value;
                //signal that the property has changed
                Debug.WriteLine($"SelectedWorkout set to: {_selectedWorkout?.Name}"); // Debug message
                OnPropertyChanged();

                if (_selectedWorkout != null)
                {
                    //update the collection
                    IList<CompleteWorkoutModel> complWorkouts = FilledCompleteWorkoutsWithExercies(
                        this._completeWorkoutService.GetCompleteWorkoutsByWorkoutId(this._selectedWorkout.Id));
                    CompleteWorkouts = new ObservableCollection<CompleteWorkoutModel>(complWorkouts);
                }
                else
                {
                    CompleteWorkouts.Clear();
                }
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

        // Default constructor for backward compatibility
        public SelectedWorkoutViewModel() : this(
            new WorkoutService(),
            new ExerciseService(),
            new CompleteWorkoutService())
        {
        }

        // Constructor with dependency injection
        public SelectedWorkoutViewModel(
            IWorkoutService workoutService,
            IExerciseService exerciseService,
            ICompleteWorkoutService completeWorkoutService)
        {
            this._workoutService = workoutService;
            this._exerciseService = exerciseService;
            this._completeWorkoutService = completeWorkoutService;
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

        public void UpdateWorkoutName(string newName)
        {
            try
            {
                if (_selectedWorkout == null || string.IsNullOrWhiteSpace(newName))
                {
                    throw new InvalidOperationException("Workout cannot be null and name cannot be empty or null.");
                }

                _selectedWorkout.Name = newName;
                this._workoutService.UpdateWorkout(_selectedWorkout);

                // Notify the UI about the change
                OnPropertyChanged(nameof(SelectedWorkout));

                // Reload the CompleteWorkouts collection if necessary
                IList<CompleteWorkoutModel> complWorkouts = FilledCompleteWorkoutsWithExercies(this._completeWorkoutService.GetCompleteWorkoutsByWorkoutId(this._selectedWorkout.Id));
                CompleteWorkouts = new ObservableCollection<CompleteWorkoutModel>(complWorkouts);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the workout: {ex.Message}", ex);
            }
        }
    }
}
