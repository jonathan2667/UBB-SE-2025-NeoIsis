using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NeoIsisJob.Data;
using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace NeoIsisJob.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ICalendarRepository _calendarRepository;

        public CalendarService(ICalendarRepository calendarRepository = null)
        {
            _dbHelper = new DatabaseHelper();
            _calendarRepository = calendarRepository ?? new CalendarRepository();
        }

        public List<CalendarDay> GetCalendarDaysForMonth(int userId, DateTime date)
        {
            return _calendarRepository.GetCalendarDaysForMonth(userId, date);
        }

        public ObservableCollection<CalendarDay> GetCalendarDays(int userId, DateTime currentDate)
        {
            var calendarDays = new ObservableCollection<CalendarDay>();
            var monthDays = GetCalendarDaysForMonth(userId, currentDate);

            DateTime firstDay = new DateTime(currentDate.Year, currentDate.Month, 1);
            int startDayOfWeek = (int)firstDay.DayOfWeek;
            int row = 0;
            int col = 0;

            // Add empty days for the start of the month
            for (int i = 0; i < startDayOfWeek; i++)
            {
                calendarDays.Add(new CalendarDay
                {
                    IsEnabled = false,
                    GridRow = row,
                    GridColumn = col
                });
                col++;
                if (col > 6) { col = 0; row++; }
            }

            DateTime today = DateTime.Now.Date;
            foreach (var day in monthDays)
            {
                day.GridRow = row;
                day.GridColumn = col;

                if (day.HasWorkout && day.Date >= today)
                {
                    day.IsEnabled = true;
                }
                calendarDays.Add(day);
                col++;
                if (col > 6) { col = 0; row++; }
            }

            return calendarDays;
        }

        public void RemoveWorkout(int userId, CalendarDay day)
        {
            var workout = GetUserWorkout(userId, day.Date);
            if (workout != null)
            {
                DeleteUserWorkout(userId, workout.WorkoutId, day.Date);
            }
        }

        public void ChangeWorkout(int userId, CalendarDay day)
        {
            var workout = GetUserWorkout(userId, day.Date);
            if (workout != null)
            {
                DeleteUserWorkout(userId, workout.WorkoutId, day.Date);
            }
        }

        public string GetWorkoutDaysCountText(ObservableCollection<CalendarDay> calendarDays)
        {
            return $"Workout Days: {calendarDays.Count(d => d.HasWorkout)}";
        }

        public string GetDaysCountText(ObservableCollection<CalendarDay> calendarDays)
        {
            return $"Days Count: {calendarDays.Count}";
        }

        public void AddUserWorkout(UserWorkoutModel userWorkout)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                        INSERT INTO UserWorkouts (UID, WID, Date, Completed)
                        VALUES (@UID, @WID, @Date, @Completed)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", userWorkout.UserId);
                    cmd.Parameters.AddWithValue("@WID", userWorkout.WorkoutId);
                    cmd.Parameters.AddWithValue("@Date", userWorkout.Date.Date);
                    cmd.Parameters.AddWithValue("@Completed", userWorkout.Completed);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUserWorkout(UserWorkoutModel userWorkout)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                        UPDATE UserWorkouts  
                        SET Completed = @Completed
                        WHERE UID = @UID AND WID = @WID AND Date = @Date";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", userWorkout.UserId);
                    cmd.Parameters.AddWithValue("@WID", userWorkout.WorkoutId);
                    cmd.Parameters.AddWithValue("@Date", userWorkout.Date.Date);
                    cmd.Parameters.AddWithValue("@Completed", userWorkout.Completed);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                        DELETE FROM UserWorkouts  
                        WHERE UID = @UID AND WID = @WID AND Date = @Date";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", userId);
                    cmd.Parameters.AddWithValue("@WID", workoutId);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserWorkoutModel GetUserWorkout(int userId, DateTime date)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT UID, WID, Date, Completed FROM UserWorkouts WHERE UID = @UID AND Date = @Date";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", userId);
                    cmd.Parameters.AddWithValue("@Date", date);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserWorkoutModel(
                                (int)reader["UID"],
                                (int)reader["WID"],
                                (DateTime)reader["Date"],
                                (bool)reader["Completed"]
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public string GetUserClass(int userId, DateTime date)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ClassName FROM UserClasses WHERE UID = @UID AND Date = @Date";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", userId);
                    cmd.Parameters.AddWithValue("@Date", date);
                    var result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        public List<WorkoutModel> GetWorkouts()
        {
            var workouts = new List<WorkoutModel>();
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT WID, Name FROM Workouts";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            workouts.Add(new WorkoutModel
                            {
                                Id = (int)reader["WID"],
                                Name = reader["Name"].ToString()
                            });
                        }
                    }
                }
            }
            return workouts;
        }
    }
}