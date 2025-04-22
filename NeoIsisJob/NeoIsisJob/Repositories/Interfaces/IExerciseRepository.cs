using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IExerciseRepository
    {
        ExercisesModel GetExerciseById(int exerciseId);
        IList<ExercisesModel> GetAllExercises();
    }
}