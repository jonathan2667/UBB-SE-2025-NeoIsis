using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Services.Interfaces
{
    public interface IWorkoutTypeService
    {
        void InsertWorkoutType(string workoutTypeName);
        void DeleteWorkoutType(int workoutTypeId);
        WorkoutTypeModel GetWorkoutTypeById(int workoutTypeId);
        IList<WorkoutTypeModel> GetAllWorkoutTypes();
    }
} 