using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

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