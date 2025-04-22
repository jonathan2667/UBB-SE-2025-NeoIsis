﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using NeoIsisJob.Commands;
using NeoIsisJob.Models;
using NeoIsisJob.Services;

namespace NeoIsisJob.ViewModels.Workout
{
    public class CreateWorkoutViewModel : INotifyPropertyChanged
    {
        private readonly Frame frame;

        private readonly WorkoutTypeService workoutTypeService;
        private readonly ExerciseService exerciseService;
        private readonly MuscleGroupService muscleGroupService;
        private readonly WorkoutService workoutService;
        private readonly CompleteWorkoutService completeWorkoutService;
        private ObservableCollection<WorkoutTypeModel> workoutTypes;
        private ObservableCollection<ExercisesModel> exercises;

        // for the add functionality
        private string selectedWorkoutName;
        private WorkoutTypeModel selectedWorkoutType;
        private ObservableCollection<ExercisesModel> selectedExercises;
        private int selectedNumberOfSets;
        private int selectedNumberOfRepsPerSet;

        public ObservableCollection<WorkoutTypeModel> WorkoutTypes
        {
            get
            {
                return workoutTypes;
            }
            set
            {
                workoutTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ExercisesModel> Exercises
        {
            get
            {
                return exercises;
            }
            set
            {
                exercises = value;
                OnPropertyChanged();
            }
        }

        // property for the current selected workout type
        public WorkoutTypeModel SelectedWorkoutType
        {
            get
            {
                return selectedWorkoutType;
            }
            set
            {
                selectedWorkoutType = value;
                OnPropertyChanged();
            }
        }

        public string SelectedWorkoutName
        {
            get
            {
                return selectedWorkoutName;
            }
            set
            {
                selectedWorkoutName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ExercisesModel> SelectedExercises
        {
            get
            {
                return selectedExercises;
            }
            set
            {
                selectedExercises = value;
                OnPropertyChanged();
            }
        }

        public int SelectedNumberOfSets
        {
            get
            {
                return selectedNumberOfSets;
            }
            set
            {
                selectedNumberOfSets = value;
                OnPropertyChanged();
            }
        }

        public int SelectedNumberOfRepsPerSet
        {
            get
            {
                return selectedNumberOfRepsPerSet;
            }
            set
            {
                selectedNumberOfRepsPerSet = value;
                OnPropertyChanged();
            }
        }

        // command for add
        public ICommand CreateWorkoutAndCompleteWorkoutsCommand { get; }

        // command for cancel
        public ICommand CancelCommand { get; }

        public CreateWorkoutViewModel(Frame frame)
        {
            // pass the frame to the viewModel
            this.frame = frame;

            this.workoutTypeService = new WorkoutTypeService();
            this.exerciseService = new ExerciseService();
            this.muscleGroupService = new MuscleGroupService();
            this.workoutService = new WorkoutService();
            this.completeWorkoutService = new CompleteWorkoutService();
            this.WorkoutTypes = new ObservableCollection<WorkoutTypeModel>();
            this.Exercises = new ObservableCollection<ExercisesModel>();
            this.SelectedExercises = new ObservableCollection<ExercisesModel>();

            // initialize the commands
            CreateWorkoutAndCompleteWorkoutsCommand = new RelayCommand(CreateWorkoutAndCompleteWorkouts);
            CancelCommand = new RelayCommand(Cancel);

            LoadWorkoutTypes();
            LoadExercises();
        }

        public void LoadWorkoutTypes()
        {
            WorkoutTypes.Clear();

            foreach (WorkoutTypeModel workoutType in this.workoutTypeService.GetAllWorkoutTypes())
            {
                this.WorkoutTypes.Add(workoutType);
            }
        }

        public void LoadExercises()
        {
            Exercises.Clear();

            foreach (ExercisesModel exercise in this.exerciseService.GetAllExercises())
            {
                // add the corresponding MuscleGroup object to every one
                exercise.MuscleGroup = this.muscleGroupService.GetMuscleGroupById(exercise.MuscleGroupId);
                this.Exercises.Add(exercise);
            }
        }

        // function that will serve a command bound to the save button
        public void CreateWorkoutAndCompleteWorkouts()
        {
            // save the workout and then save all entries in CompleteWorkouts

            // here add the workout
            this.workoutService.InsertWorkout(SelectedWorkoutName, SelectedWorkoutType.Id);
            int selectedWorkoutId = this.workoutService.GetWorkoutByName(SelectedWorkoutName).Id;

            // here add all the entries in CompleteWorkouts
            foreach (ExercisesModel exercise in SelectedExercises)
            {
                this.completeWorkoutService.InsertCompleteWorkout(selectedWorkoutId, exercise.Id, SelectedNumberOfSets, SelectedNumberOfRepsPerSet);
            }

            // now go to back to the prev page
            if (this.frame.CanGoBack)
            {
                this.frame.GoBack();
            }
        }

        public void Cancel()
        {
            if (this.frame.CanGoBack)
            {
                this.frame.GoBack();
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        // gets triggered every time a property changes
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
