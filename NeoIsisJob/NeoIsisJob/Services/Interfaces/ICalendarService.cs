using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NeoIsisJob.Models;

namespace NeoIsisJob.Services.Interfaces
{
    public interface ICalendarService
    {
        /// <summary>
        /// Retrieves the calendar days for a given user and month.
        /// </summary>
        List<CalendarDay> GetCalendarDaysForMonth(int userId, DateTime date);

        /// <summary>
        /// Inserts a new workout for the user.
        /// </summary>
        void AddUserWorkout(UserWorkoutModel userWorkout);

        /// <summary>
        /// Updates an existing workout.
        /// </summary>
        void UpdateUserWorkout(UserWorkoutModel userWorkout);

        /// <summary>
        /// Deletes a workout based on user, workout identifier, and date.
        /// </summary>
        void DeleteUserWorkout(int userId, int workoutId, DateTime date);

        /// <summary>
        /// Gets a specific workout for a user on a given date.
        /// </summary>
        UserWorkoutModel GetUserWorkout(int userId, DateTime date);

        /// <summary>
        /// Retrieves the calendar days for a given user and date.
        /// Includes logic to calculate the grid layout.
        /// </summary>
        ObservableCollection<CalendarDay> GetCalendarDays(int userId, DateTime currentDate);

        /// <summary>
        /// Removes a workout for the specified day (if one exists).
        /// </summary>
        void RemoveWorkout(int userId, CalendarDay day);

        /// <summary>
        /// Changes a workout for the specified day.
        /// </summary>
        void ChangeWorkout(int userId, CalendarDay day);

        /// <summary>
        /// Gets the text representation of workout days count.
        /// </summary>
        string GetWorkoutDaysCountText(ObservableCollection<CalendarDay> calendarDays);

        /// <summary>
        /// Gets the text representation of total days count.
        /// </summary>
        string GetDaysCountText(ObservableCollection<CalendarDay> calendarDays);

        /// <summary>
        /// Gets the class name for a user on a given date.
        /// </summary>
        string GetUserClass(int userId, DateTime date);

        /// <summary>
        /// Gets all available workouts.
        /// </summary>
        List<WorkoutModel> GetWorkouts();
    }
}
