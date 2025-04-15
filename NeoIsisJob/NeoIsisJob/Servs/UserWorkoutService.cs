using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using NeoIsisJob.Repos.Interfaces;
using NeoIsisJob.Servs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoIsisJob.Servs
{
    public class UserWorkoutService : IUserWorkoutService
    {
        private readonly IUserWorkoutRepository _userWorkoutRepository;

        public UserWorkoutService()
        {
            _userWorkoutRepository = new UserWorkoutRepo(new Data.DatabaseHelper());
        }

        public UserWorkoutService(IUserWorkoutRepository userWorkoutRepository)
        {
            _userWorkoutRepository = userWorkoutRepository ?? throw new ArgumentNullException(nameof(userWorkoutRepository));
        }

        public UserWorkoutModel GetUserWorkoutForDate(int userId, DateTime date)
        {
            var userWorkouts = _userWorkoutRepository.GetUserWorkoutModelByDate(date);
            return userWorkouts.FirstOrDefault(userWorkout => userWorkout.UserId == userId);
        }

        public void AddUserWorkout(UserWorkoutModel userWorkout)
        {
            // First, check if there's already a workout for this date
            var existingWorkout = GetUserWorkoutForDate(userWorkout.UserId, userWorkout.Date);

            if (existingWorkout != null)
            {
                // If there's an existing workout, update it
                _userWorkoutRepository.UpdateUserWorkout(userWorkout);
            }
            else
            {
                // Otherwise, add a new one
                _userWorkoutRepository.AddUserWorkout(userWorkout);
            }
        }

        public void CompleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            var userWorkout = _userWorkoutRepository.GetUserWorkoutModel(userId, workoutId, date);

            if (userWorkout != null)
            {
                userWorkout.Completed = true;
                _userWorkoutRepository.UpdateUserWorkout(userWorkout);
            }
        }

        public void DeleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            _userWorkoutRepository.DeleteUserWorkout(userId, workoutId, date);
        }
    }
}