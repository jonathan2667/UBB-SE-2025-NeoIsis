using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NeoIsisJob.Data;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using NeoIsisJob.Services.Interfaces;
using NeoIsisJob.Repositories.Interfaces;

namespace NeoIsisJob.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly UserWorkoutRepo _userWorkoutRepo;

        public CalendarService(ICalendarRepository calendarRepository = null, UserWorkoutRepo userWorkoutRepo = null)
        {
            _calendarRepository = calendarRepository ?? new CalendarRepository();
            _userWorkoutRepo = userWorkoutRepo ?? new UserWorkoutRepo(new DatabaseHelper());
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
            _userWorkoutRepo.AddUserWorkout(userWorkout);
        }

        public void UpdateUserWorkout(UserWorkoutModel userWorkout)
        {
            _userWorkoutRepo.UpdateUserWorkout(userWorkout);
        }

        public void DeleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            _userWorkoutRepo.DeleteUserWorkout(userId, workoutId, date);
        }

        public UserWorkoutModel GetUserWorkout(int userId, DateTime date)
        {
            return _calendarRepository.GetUserWorkout(userId, date);
        }

        public string GetUserClass(int userId, DateTime date)
        {
            return _calendarRepository.GetUserClass(userId, date);
        }

        public List<WorkoutModel> GetWorkouts()
        {
            return _calendarRepository.GetWorkouts();
        }
    }
}