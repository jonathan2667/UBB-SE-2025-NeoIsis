using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Services.Interfaces
{
    public interface IExerciseService
    {
        ExercisesModel GetExerciseById(int exerciseId);
        IList<ExercisesModel> GetAllExercises();
    }
}