using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NeoIsisJob.Models;
using NeoIsisJob.Services;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.ViewModels.Workout
{
    public class SelectedWorkoutViewModel : INotifyPropertyChanged
    {
        private readonly IWorkoutService workoutService;
        // for getting the exercise by id
        private readonly IExerciseService exerciseService;
        private readonly ICompleteWorkoutService completeWorkoutService;
        private WorkoutModel selectedWorkout;
        private ObservableCollection<CompleteWorkoutModel> completeWorkouts;

        public WorkoutModel SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                // signal that the property has changed
                Debug.WriteLine($"SelectedWorkout set to: {selectedWorkout?.Name}"); // Debug message
                OnPropertyChanged();

                if (selectedWorkout != null)
                {
                    // update the collection
                    IList<CompleteWorkoutModel> completeWorkouts = FilledCompleteWorkoutsWithExercies(
                        this.completeWorkoutService.GetCompleteWorkoutsByWorkoutId(this.selectedWorkout.Id));
                    CompleteWorkouts = new ObservableCollection<CompleteWorkoutModel>(completeWorkouts);
                }
                else
                {
                    CompleteWorkouts.Clear();
                }
            }
        }

        public ObservableCollection<CompleteWorkoutModel> CompleteWorkouts
        {
            get => completeWorkouts;
            set
            {
                completeWorkouts = value;
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
            this.workoutService = workoutService;
            this.exerciseService = exerciseService;
            this.completeWorkoutService = completeWorkoutService;
            this.completeWorkouts = new ObservableCollection<CompleteWorkoutModel>();
        }

        public IList<CompleteWorkoutModel> FilledCompleteWorkoutsWithExercies(IList<CompleteWorkoutModel> complWorkouts)
        {
            foreach (CompleteWorkoutModel complWorkout in complWorkouts)
            {
                complWorkout.Exercise = this.exerciseService.GetExerciseById(complWorkout.ExerciseId);
            }

            return complWorkouts;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        // gets triggered every time a property changes
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateWorkoutName(string newName)
        {
            try
            {
                if (selectedWorkout == null || string.IsNullOrWhiteSpace(newName))
                {
                    throw new InvalidOperationException("Workout cannot be null and name cannot be empty or null.");
                }

                selectedWorkout.Name = newName;
                this.workoutService.UpdateWorkout(selectedWorkout);

                // Notify the UI about the change
                OnPropertyChanged(nameof(SelectedWorkout));

                // Reload the CompleteWorkouts collection if necessary
                IList<CompleteWorkoutModel> complWorkouts = FilledCompleteWorkoutsWithExercies(this.completeWorkoutService.GetCompleteWorkoutsByWorkoutId(this.selectedWorkout.Id));
                CompleteWorkouts = new ObservableCollection<CompleteWorkoutModel>(complWorkouts);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the workout: {ex.Message}", ex);
            }
        }
    }
}
