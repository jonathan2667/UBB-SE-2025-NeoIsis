using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Services.Interfaces
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