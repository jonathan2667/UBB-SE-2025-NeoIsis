// Repositories/CalendarRepository.cs
using NeoIsisJob.Models;
using NeoIsisJob.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.Design;
using NeoIsisJob.Repositories.Interfaces;

namespace NeoIsisJob.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly DatabaseHelper _databaseHelper;
        private const int FirstDayOfMonth = 1;
        private const int StartEndMonthDifference = 1;
        private const int StartEndDayDifference = -1;

        public CalendarRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public List<CalendarDay> GetCalendarDaysForMonth(int userId, DateTime month)
        {
            var calendarDays = new List<CalendarDay>();
            DateTime firstDay = new DateTime(month.Year, month.Month, FirstDayOfMonth);
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            using (var databaseConnection = _databaseHelper.GetConnection())
            {
                databaseConnection.Open();
                string query = @"
                SELECT Date, WID, Completed 
                FROM UserWorkouts 
                WHERE UID = @UserId 
                AND Date >= @StartDate 
                AND Date <= @EndDate";
                int UserWorkoutsDateColumn = 0;
                int UserWorkoutsCompletedColumn = 2;

                var workoutDays = new Dictionary<DateTime, (bool HasWorkout, bool Completed)>();

                using (var command = new SqlCommand(query, databaseConnection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@StartDate", firstDay);
                    command.Parameters.AddWithValue("@EndDate", firstDay.AddMonths(StartEndMonthDifference).AddDays(StartEndDayDifference));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = reader.GetDateTime(UserWorkoutsDateColumn);
                            workoutDays[date] = (true, reader.GetBoolean(UserWorkoutsCompletedColumn));
                            System.Diagnostics.Debug.WriteLine($"Found workout: Date={date:yyyy-MM-dd}, Completed={reader.GetBoolean(UserWorkoutsCompletedColumn)}");
                        }
                    }
                }


                string classQuery = @"
                SELECT Date 
                FROM UserClasses 
                WHERE UID = @UserId 
                AND Date >= @StartDate 
                AND Date <= @EndDate";
                int UserClassesDateColumn = 0;

                var classDays = new Dictionary<DateTime, bool>();  // Using HashSet for efficient lookup
                using (var command = new SqlCommand(classQuery, databaseConnection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@StartDate", firstDay);
                    command.Parameters.AddWithValue("@EndDate", firstDay.AddMonths(StartEndMonthDifference).AddDays(StartEndDayDifference));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = reader.GetDateTime(UserClassesDateColumn);
                            classDays[date] = (true);
                            System.Diagnostics.Debug.WriteLine($"Found class: Date={date:yyyy-MM-dd}");
                        }
                    }
                }
                for (int day = FirstDayOfMonth; day <= daysInMonth; day++)
                {
                    var currentDate = new DateTime(month.Year, month.Month, day);
                    bool hasWorkout = workoutDays.ContainsKey(currentDate);
                    bool isCompleted = hasWorkout && workoutDays[currentDate].Completed;
                    bool hasClass = classDays.ContainsKey(currentDate);

                    calendarDays.Add(new CalendarDay {
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
            using (var databaseConnection = _databaseHelper.GetConnection())
            {
                databaseConnection.Open();
                string query = @"
                SELECT WID, Completed 
                FROM UserWorkouts 
                WHERE UID = @UserId AND Date = @Date";
                int WorkoutIDColumn = 0;
                int CompletedColumn = 1;

                using (var command = new SqlCommand(query, databaseConnection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Date", date.Date);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserWorkoutModel(
                                userId: userId,
                                workoutId: reader.GetInt32(WorkoutIDColumn),
                                date: date,
                                completed: reader.GetBoolean(CompletedColumn)
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public String GetUserClass(int userId, DateTime date)
        {
            using (var databaseConnection = _databaseHelper.GetConnection())
            {
                databaseConnection.Open();

                // Step 1: Query UserClasses to get CID
                string classQuery = @"
                SELECT CID
                FROM UserClasses 
                WHERE UID = @UserId AND Date = @Date";
                int UserClassIDColumn = 0;

                int? classId = null;
                using (var command = new SqlCommand(classQuery, databaseConnection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Date", date.Date);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            classId = reader.GetInt32(UserClassIDColumn); // Get CID
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
                int ClassNameColumn = 0;

                using (var command = new SqlCommand(detailsQuery, databaseConnection))
                {
                    command.Parameters.AddWithValue("@ClassId", classId.Value);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Since UserClasses doesn't have Completed, we'll default it to false
                            // Adjust this if Classes has a relevant field
                            return reader.GetString(ClassNameColumn);
                        }
                        return null; // Class not found in Classes table
                    }
                }
            }
        }

        public List<WorkoutModel> GetWorkouts()
        {
            using (var databaseConnection = _databaseHelper.GetConnection())
            {
                databaseConnection.Open();
                string query = "SELECT WID, Name FROM Workouts";
                int WorkoutIDColumn = 0;
                int WorkoutNameColumn = 1;

                using (var command = new SqlCommand(query, databaseConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var workouts = new List<WorkoutModel>();
                        while (reader.Read())
                        {
                            workouts.Add(new WorkoutModel
                            {
                                Id = reader.GetInt32(WorkoutIDColumn),
                                Name = reader.GetString(WorkoutNameColumn)
                            });
                        }
                        return workouts;
                    }
                }
            }
        }
    }
}