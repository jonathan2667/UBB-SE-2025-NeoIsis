using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

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