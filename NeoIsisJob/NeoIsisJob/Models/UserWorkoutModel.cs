using System;

namespace NeoIsisJob.Models
{
    public class UserWorkoutModel
    {
        private int _userId;
        private int _workoutId;
        private DateTime _date;
        private bool _completed;

        public int UserId { get => _userId; set => _userId = value; }
        public int WorkoutId { get => _workoutId; set => _workoutId = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public bool Completed { get => _completed; set => _completed = value; }

        private UserWorkoutModel() { }

        public UserWorkoutModel(int userId, int workoutId, DateTime date, bool completed)
        {
            UserId = userId;
            WorkoutId = workoutId;
            Date = date;
            Completed = completed;
        }
    }
}
