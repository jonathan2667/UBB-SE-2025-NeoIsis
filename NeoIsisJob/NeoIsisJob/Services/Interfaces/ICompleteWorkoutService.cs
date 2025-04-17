using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Services.Interfaces
{
    public interface ICompleteWorkoutService
    {
        IList<CompleteWorkoutModel> GetAllCompleteWorkouts();
        IList<CompleteWorkoutModel> GetCompleteWorkoutsByWorkoutId(int workoutId);
        void DeleteCompleteWorkoutsByWorkoutId(int workoutId);
        void InsertCompleteWorkout(int workoutId, int exerciseId, int sets, int repetitionsPerSet);
    }
}