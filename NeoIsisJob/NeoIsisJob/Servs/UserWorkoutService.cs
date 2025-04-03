using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoIsisJob.Servs
{
    public class UserWorkoutService
    {
        private readonly UserWorkoutRepo _userWorkoutRepo;

        public UserWorkoutService()
        {
            _userWorkoutRepo = new UserWorkoutRepo(new Data.DatabaseHelper());
        }

        public UserWorkoutModel GetUserWorkoutForDate(int userId, DateTime date)
        {
            var userWorkouts = _userWorkoutRepo.GetUserWorkoutModelByDate(date);
            return userWorkouts.FirstOrDefault(uw => uw.UserId == userId);
        }

        public void AddUserWorkout(UserWorkoutModel userWorkout)
        {
            // First, check if there's already a workout for this date
            var existingWorkout = GetUserWorkoutForDate(userWorkout.UserId, userWorkout.Date);

            if (existingWorkout != null)
            {
                // If there's an existing workout, update it
                _userWorkoutRepo.UpdateUserWorkout(userWorkout);
            }
            else
            {
                // Otherwise, add a new one
                _userWorkoutRepo.AddUserWorkout(userWorkout);
            }
        }

        public void CompleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            var userWorkout = _userWorkoutRepo.GetUserWorkoutModel(userId, workoutId, date);

            if (userWorkout != null)
            {
                userWorkout.Completed = true;
                _userWorkoutRepo.UpdateUserWorkout(userWorkout);
            }
        }

        public void DeleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            _userWorkoutRepo.DeleteUserWorkout(userId, workoutId, date);
        }
    }
}