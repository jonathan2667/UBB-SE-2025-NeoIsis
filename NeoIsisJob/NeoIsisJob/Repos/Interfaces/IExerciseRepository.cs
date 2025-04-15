using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Repos.Interfaces
{
    public interface IExerciseRepository
    {
        ExercisesModel GetExerciseById(int exerciseId);
        IList<ExercisesModel> GetAllExercises();
    }
} 