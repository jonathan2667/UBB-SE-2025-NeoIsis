using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

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