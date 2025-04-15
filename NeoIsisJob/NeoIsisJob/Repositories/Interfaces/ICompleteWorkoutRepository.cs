using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface ICompleteWorkoutRepository
    {
        IList<CompleteWorkoutModel> GetAllCompleteWorkouts();
        void DeleteCompleteWorkoutsByWorkoutId(int workoutId);
        void InsertCompleteWorkout(int workoutId, int exerciseId, int sets, int repetitionsPerSet);
    }
} 