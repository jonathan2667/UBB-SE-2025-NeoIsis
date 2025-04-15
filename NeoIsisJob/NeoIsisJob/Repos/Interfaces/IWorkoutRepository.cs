using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Repos.Interfaces
{
    public interface IWorkoutRepository
    {
        WorkoutModel GetWorkoutById(int workoutId);
        WorkoutModel GetWorkoutByName(string workoutName);
        void InsertWorkout(string workoutName, int workoutTypeId);
        void DeleteWorkout(int workoutId);
        void UpdateWorkout(WorkoutModel workout);
        IList<WorkoutModel> GetAllWorkouts();
    }
} 