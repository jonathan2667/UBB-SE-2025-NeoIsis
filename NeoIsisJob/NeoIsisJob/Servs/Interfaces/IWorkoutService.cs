using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Servs.Interfaces
{
    public interface IWorkoutService
    {
        WorkoutModel GetWorkout(int workoutId);
        WorkoutModel GetWorkoutByName(string workoutName);
        void InsertWorkout(string workoutName, int workoutTypeId);
        void DeleteWorkout(int workoutId);
        void UpdateWorkout(WorkoutModel workout);
        IList<WorkoutModel> GetAllWorkouts();
    }
} 