﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using NeoIsisJob.Models;
using NeoIsisJob.Services;
using NeoIsisJob.Services.Interfaces;
using NeoIsisJob.Commands;

namespace NeoIsisJob.ViewModels.Workout
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        // Use interfaces instead of concrete implementations
        private readonly IWorkoutService workoutService;
        private readonly IWorkoutTypeService workoutTypeService;
        private readonly ICompleteWorkoutService completeWorkoutService;
        private readonly IExerciseService exerciseService;
        private ObservableCollection<WorkoutModel> workouts;
        private ObservableCollection<WorkoutTypeModel> workoutTypes;
        private WorkoutTypeModel selectedWorkoutType;

        // Add SelectedWorkoutViewModel as a property
        public SelectedWorkoutViewModel SelectedWorkoutViewModel { get; }

        // Add commands for deleting, updating, and closing the edit popup
        public ICommand DeleteWorkoutCommand { get; }
        public ICommand UpdateWorkoutCommand { get; }
        public ICommand CloseEditPopupCommand { get; }

        // Default constructor for backward compatibility
        public WorkoutViewModel() : this(
            new WorkoutService(),
            new WorkoutTypeService(),
            new CompleteWorkoutService(),
            new ExerciseService())
        {
        }

        // Constructor with dependency injection
        public WorkoutViewModel(
            IWorkoutService workoutService,
            IWorkoutTypeService workoutTypeService,
            ICompleteWorkoutService completeWorkoutService,
            IExerciseService exerciseService)
        {
            this.workoutService = workoutService;
            this.workoutTypeService = workoutTypeService;
            this.completeWorkoutService = completeWorkoutService;
            this.exerciseService = exerciseService;

            Workouts = new ObservableCollection<WorkoutModel>();
            WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();

            // Initialize SelectedWorkoutViewModel with dependencies
            SelectedWorkoutViewModel = new SelectedWorkoutViewModel(
                workoutService,
                exerciseService,
                completeWorkoutService);

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
            get => workouts;
            set
            {
                workouts = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WorkoutTypeModel> WorkoutTypes
        {
            get => workoutTypes;
            set
            {
                workoutTypes = value;
                OnPropertyChanged();
            }
        }

        public WorkoutTypeModel SelectedWorkoutType
        {
            get => selectedWorkoutType;
            set
            {
                selectedWorkoutType = value;
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

        private bool isEditPopupOpen;
        public bool IsEditPopupOpen
        {
            get => isEditPopupOpen;
            set
            {
                isEditPopupOpen = value;
                OnPropertyChanged();
            }
        }

        private void LoadWorkouts()
        {
            Workouts.Clear();

            foreach (var workout in this.workoutService.GetAllWorkouts())
            {
                Workouts.Add(workout);
            }
        }

        private void LoadWorkoutTypes()
        {
            WorkoutTypes.Clear();
            foreach (var workoutType in this.workoutTypeService.GetAllWorkoutTypes())
            {
                WorkoutTypes.Add(workoutType);
            }
        }

        private void ApplyWorkoutFilter()
        {
            Workouts.Clear();
            IList<WorkoutModel> allWorkouts = this.workoutService.GetAllWorkouts();

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
            this.completeWorkoutService.DeleteCompleteWorkoutsByWorkoutId(workoutId);
            this.workoutService.DeleteWorkout(workoutId);

            // Loading workouts again
            LoadWorkouts();
        }

        public void UpdateWorkout(string newName)
        {
            // Update the selected workout's name
            if (SelectedWorkout != null && !string.IsNullOrWhiteSpace(newName))
            {
                SelectedWorkout.Name = newName;
                workoutService.UpdateWorkout(SelectedWorkout);
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