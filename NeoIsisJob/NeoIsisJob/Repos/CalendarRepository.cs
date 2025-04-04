// Repos/CalendarRepository.cs
using NeoIsisJob.Models;
using NeoIsisJob.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NeoIsisJob.Repos
{
    public interface ICalendarRepository
    {
        List<CalendarDay> GetCalendarDaysForMonth(int userId, DateTime month);
        UserWorkoutModel GetUserWorkout(int userId, DateTime date);
        List<WorkoutModel> GetWorkouts();

        String GetUserClass(int userId, DateTime date);
    }

    public class CalendarRepository : ICalendarRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public CalendarRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public List<CalendarDay> GetCalendarDaysForMonth(int userId, DateTime month)
        {
            var calendarDays = new List<CalendarDay>();
            DateTime firstDay = new DateTime(month.Year, month.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT Date, WID, Completed 
            FROM UserWorkouts 
            WHERE UID = @UserId 
            AND Date >= @StartDate 
            AND Date <= @EndDate";
                var workoutDays = new Dictionary<DateTime, (bool HasWorkout, bool Completed)>();

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@StartDate", firstDay);
                    cmd.Parameters.AddWithValue("@EndDate", firstDay.AddMonths(1).AddDays(-1));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = reader.GetDateTime(0);
                            workoutDays[date] = (true, reader.GetBoolean(2));
                            System.Diagnostics.Debug.WriteLine($"Found workout: Date={date:yyyy-MM-dd}, Completed={reader.GetBoolean(2)}");
                        }
                    }
                }
                string classQuery = @"
            SELECT Date 
            FROM UserClasses 
            WHERE UID = @UserId 
            AND Date >= @StartDate 
            AND Date <= @EndDate";

                var classDays = new Dictionary<DateTime, bool>();  // Using HashSet for efficient lookup
                using (var cmd = new SqlCommand(classQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@StartDate", firstDay);
                    cmd.Parameters.AddWithValue("@EndDate", firstDay.AddMonths(1).AddDays(-1));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = reader.GetDateTime(0);
                            classDays[date] = (true);
                            System.Diagnostics.Debug.WriteLine($"Found class: Date={date:yyyy-MM-dd}");
                        }
                    }
                }
                for (int day = 1; day <= daysInMonth; day++)
                        {
                            var currentDate = new DateTime(month.Year, month.Month, day);
                            bool hasWorkout = workoutDays.ContainsKey(currentDate);
                            bool isCompleted = hasWorkout && workoutDays[currentDate].Completed;
                            bool hasClass = classDays.ContainsKey(currentDate);

                    calendarDays.Add(new CalendarDay
                            {
                                DayNumber = day,
                                Date = currentDate,
                                IsCurrentDay = currentDate.Date == DateTime.Now.Date,
                                HasWorkout = hasWorkout,
                                IsWorkoutCompleted = isCompleted,
                                HasClass = hasClass
                    });
                        }
                    
                
            }
            return calendarDays;
        }

        public UserWorkoutModel GetUserWorkout(int userId, DateTime date)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                SELECT WID, Completed 
                FROM UserWorkouts 
                WHERE UID = @UserId AND Date = @Date";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Date", date.Date);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserWorkoutModel(
                                userId: userId,
                                workoutId: reader.GetInt32(0),
                                date: date,
                                completed: reader.GetBoolean(1)
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public String GetUserClass(int userId, DateTime date)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();

                // Step 1: Query UserClasses to get CID
                string classQuery = @"
            SELECT CID
            FROM UserClasses 
            WHERE UID = @UserId AND Date = @Date";

                int? classId = null;
                using (var cmd = new SqlCommand(classQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Date", date.Date);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            classId = reader.GetInt32(0); // Get CID
                        }
                    }
                }

                // If no class found, return null
                if (!classId.HasValue)
                {
                    return null;
                }

                // Step 2: Query Classes table using CID
                string detailsQuery = @"
            SELECT Name 
            FROM Classes 
            WHERE CID = @ClassId";

                using (var cmd = new SqlCommand(detailsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ClassId", classId.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Since UserClasses doesn't have Completed, we'll default it to false
                            // Adjust this if Classes has a relevant field
                            return reader.GetString(0);
                        }
                        return null; // Class not found in Classes table
                    }
                }
            }
        }

        public List<WorkoutModel> GetWorkouts()
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT WID, Name FROM Workouts";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var workouts = new List<WorkoutModel>();
                        while (reader.Read())
                        {
                            workouts.Add(new WorkoutModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                        return workouts;
                    }
                }
            }
        }
    }
}