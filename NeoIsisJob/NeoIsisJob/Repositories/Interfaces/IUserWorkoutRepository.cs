using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IUserWorkoutRepository
    {
        List<UserWorkoutModel> GetUserWorkoutModelByDate(DateTime date);
        UserWorkoutModel GetUserWorkoutModel(int userId, int workoutId, DateTime date);
        void AddUserWorkout(UserWorkoutModel userWorkout);
        void UpdateUserWorkout(UserWorkoutModel userWorkout);
        void DeleteUserWorkout(int userId, int workoutId, DateTime date);
    }
} 