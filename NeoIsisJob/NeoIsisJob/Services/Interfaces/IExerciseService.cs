using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Services.Interfaces
{
    public interface IExerciseService
    {
        ExercisesModel GetExerciseById(int exerciseId);
        IList<ExercisesModel> GetAllExercises();
    }
} 