﻿using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace NeoIsisJob.ViewModels.Workout
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        private readonly WorkoutService _workoutService;
        private readonly WorkoutTypeService _workoutTypeService;
        private readonly CompleteWorkoutService _completeWorkoutService;
        private ObservableCollection<WorkoutModel> _workouts;
        private ObservableCollection<WorkoutTypeModel> _workoutTypes;
        private WorkoutTypeModel _selectedWorkoutType;

        // Add SelectedWorkoutViewModel as a property
        public SelectedWorkoutViewModel SelectedWorkoutViewModel { get; }

        public WorkoutViewModel()
        {
            this._workoutService = new WorkoutService();
            this._workoutTypeService = new WorkoutTypeService();
            this._completeWorkoutService = new CompleteWorkoutService();
            Workouts = new ObservableCollection<WorkoutModel>();
            WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();

            // Initialize SelectedWorkoutViewModel
            SelectedWorkoutViewModel = new SelectedWorkoutViewModel();

            // Load workouts and workout types
            LoadWorkouts();
            LoadWorkoutTypes();

            Debug.WriteLine("WorkoutViewModel initialized.");
        }

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

        public void DeleteWorkout(int workoutId)
        {
            Debug.WriteLine($"DeleteWorkout called with Workout ID: {workoutId}");

            // Find the workout to delete
            var workoutToDelete = Workouts.FirstOrDefault(w => w.Id == workoutId);
            if (workoutToDelete != null)
            {
                // Remove the workout from the collection
                Workouts.Remove(workoutToDelete);
                Debug.WriteLine($"Workout deleted: {workoutToDelete.Name} (Id: {workoutToDelete.Id})");

                // Optionally, call a service to delete the workout from the database
                _workoutService.DeleteWorkout(workoutId);
            }
            else
            {
                Debug.WriteLine($"Workout with ID {workoutId} not found.");
            }

            // Log the updated Workouts collection
            Debug.WriteLine($"Workouts after deletion: {string.Join(", ", Workouts.Select(w => w.Name))}");
        }

        // Expose SelectedWorkout from SelectedWorkoutViewModel
        public WorkoutModel SelectedWorkout
        {
            get => SelectedWorkoutViewModel.SelectedWorkout;
            set => SelectedWorkoutViewModel.SelectedWorkout = value;
        }

        // Expose UpdateWorkoutName from SelectedWorkoutViewModel
        public void UpdateWorkoutName(string newName)
        {
            SelectedWorkoutViewModel.UpdateWorkoutName(newName);
            // Reload workouts after updating the name
            LoadWorkouts();
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}