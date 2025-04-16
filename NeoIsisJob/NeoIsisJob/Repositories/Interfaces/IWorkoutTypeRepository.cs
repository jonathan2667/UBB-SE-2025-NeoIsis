using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IWorkoutTypeRepository
    {
        WorkoutTypeModel GetWorkoutTypeById(int workoutTypeId);
        void InsertWorkoutType(string workoutTypeName);
        void DeleteWorkoutType(int workoutTypeId);
        IList<WorkoutTypeModel> GetAllWorkoutTypes();
    }
} 