using System;

namespace NeoIsisJob.Models
{
    public class UserWorkoutModel
    {
        private int _userId;
        private int _workoutId;
        private DateTime _date;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public int WorkoutId
        {
            get { return _workoutId; }
            set { _workoutId = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private UserWorkoutModel() { }

        public UserWorkoutModel(int userId, int workoutId, DateTime date)
        {
            UserId = userId;
            WorkoutId = workoutId;
            Date = date;
        }
    }
}
