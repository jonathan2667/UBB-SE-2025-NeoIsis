using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

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