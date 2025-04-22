using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
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