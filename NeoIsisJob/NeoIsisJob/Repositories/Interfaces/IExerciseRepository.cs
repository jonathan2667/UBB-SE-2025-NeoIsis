using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IExerciseRepository
    {
        ExercisesModel GetExerciseById(int exerciseId);
        IList<ExercisesModel> GetAllExercises();
    }
} 