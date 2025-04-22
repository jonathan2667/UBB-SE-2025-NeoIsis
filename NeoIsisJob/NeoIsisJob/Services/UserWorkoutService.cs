using System;
using System.Collections.Generic;
using System.Linq;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.Services
{
    public class UserWorkoutService : IUserWorkoutService
    {
        private readonly IUserWorkoutRepository userWorkoutRepository;

        public UserWorkoutService()
        {
            userWorkoutRepository = new UserWorkoutRepo(new Data.DatabaseHelper());
        }

        public UserWorkoutService(IUserWorkoutRepository userWorkoutRepository)
        {
            this.userWorkoutRepository = userWorkoutRepository; // ?? throw new ArgumentNullException(nameof(userWorkoutRepository));
        }

        public UserWorkoutModel GetUserWorkoutForDate(int userId, DateTime date)
        {
            var userWorkouts = userWorkoutRepository.GetUserWorkoutModelByDate(date);
            return userWorkouts.FirstOrDefault(userWorkout => userWorkout.UserId == userId);
        }

        public void AddUserWorkout(UserWorkoutModel userWorkout)
        {
            // First, check if there's already a workout for this date
            var existingWorkout = GetUserWorkoutForDate(userWorkout.UserId, userWorkout.Date);

            if (existingWorkout != null)
            {
                // If there's an existing workout, update it
                userWorkoutRepository.UpdateUserWorkout(userWorkout);
            }
            else
            {
                // Otherwise, add a new one
                userWorkoutRepository.AddUserWorkout(userWorkout);
            }
        }

        public void CompleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            var userWorkout = userWorkoutRepository.GetUserWorkoutModel(userId, workoutId, date);

            if (userWorkout != null)
            {
                userWorkout.Completed = true;
                userWorkoutRepository.UpdateUserWorkout(userWorkout);
            }
        }

        public void DeleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            userWorkoutRepository.DeleteUserWorkout(userId, workoutId, date);
        }
    }
}