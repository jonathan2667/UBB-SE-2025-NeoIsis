using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface ICompleteWorkoutRepository
    {
        IList<CompleteWorkoutModel> GetAllCompleteWorkouts();
        void DeleteCompleteWorkoutsByWorkoutId(int workoutId);
        void InsertCompleteWorkout(int workoutId, int exerciseId, int sets, int repetitionsPerSet);
    }
}