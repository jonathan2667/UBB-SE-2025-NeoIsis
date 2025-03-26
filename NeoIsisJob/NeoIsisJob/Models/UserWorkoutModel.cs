using System;

namespace NeoIsisJob.Models
{
    public class UserWorkoutModel
    {
        public int UserId { get; private set; }
        public int WorkoutId { get; private set; }
        public DateTime Date { get; private set; }

        private UserWorkoutModel() { }

        public UserWorkoutModel(int userId, int workoutId, DateTime date)
        {
            UserId = userId;
            WorkoutId = workoutId;
            Date = date;
        }
    }
}
