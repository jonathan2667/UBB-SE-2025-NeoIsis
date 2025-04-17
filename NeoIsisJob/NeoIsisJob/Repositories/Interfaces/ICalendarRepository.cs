using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface ICalendarRepository
    {
        List<CalendarDay> GetCalendarDaysForMonth(int userId, DateTime month);
        UserWorkoutModel GetUserWorkout(int userId, DateTime date);
        List<WorkoutModel> GetWorkouts();
        string GetUserClass(int userId, DateTime date);
    }
}