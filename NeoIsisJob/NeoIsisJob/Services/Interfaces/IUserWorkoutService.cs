using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Services.Interfaces
{
    public interface IUserWorkoutService
    {
        UserWorkoutModel GetUserWorkoutForDate(int userId, DateTime date);
        void AddUserWorkout(UserWorkoutModel userWorkout);
        void CompleteUserWorkout(int userId, int workoutId, DateTime date);
        void DeleteUserWorkout(int userId, int workoutId, DateTime date);
    }
}