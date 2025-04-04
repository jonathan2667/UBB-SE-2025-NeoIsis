// ViewModels/Calendar/CalendarViewModel.cs
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Input;
using NeoIsisJob.Models;
using NeoIsisJob.Data;
using NeoIsisJob.Repos;
using System.Linq;

namespace NeoIsisJob.ViewModels.Calendar
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTime _currentDate;
        private string _yearText;
        private string _monthText;
        private ObservableCollection<CalendarDay> _calendarDays;
        public readonly ICalendarRepository _calendarRepository;
        private readonly DatabaseHelper _dbHelper;
        public readonly int _userId;
        public string WorkoutDaysCountText => $"Workout Days: {CalendarDays.Count(d => d.HasWorkout)}"; // New property

        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarViewModel(int userId, ICalendarRepository calendarRepository = null)
        {
            _userId = userId;
            _currentDate = DateTime.Now;
            _calendarRepository = calendarRepository ?? new CalendarRepository();
            _dbHelper = new DatabaseHelper();
            CalendarDays = new ObservableCollection<CalendarDay>();
            PreviousMonthCommand = new RelayCommand(PreviousMonth);
            NextMonthCommand = new RelayCommand(NextMonth);
            UpdateCalendar();
        }

        public string YearText
        {
            get => _yearText;
            set
            {
                _yearText = value;
                OnPropertyChanged(nameof(YearText));
            }
        }

        public string MonthText
        {
            get => _monthText;
            set
            {
                _monthText = value;
                OnPropertyChanged(nameof(MonthText));
            }
        }

        public ObservableCollection<CalendarDay> CalendarDays
        {
            get => _calendarDays;
            set
            {
                _calendarDays = value;
                OnPropertyChanged(nameof(CalendarDays));
                OnPropertyChanged(nameof(DaysCountText)); // Update the new property
            }
        }

        public string DaysCountText => $"Days Count: {CalendarDays.Count}";
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        private void UpdateCalendar()
    {
        YearText = _currentDate.Year.ToString();
        MonthText = _currentDate.ToString("MMMM");

        CalendarDays.Clear();
        var monthDays = _calendarRepository.GetCalendarDaysForMonth(_userId, _currentDate);

        // Log for debugging
        System.Diagnostics.Debug.WriteLine($"Total days fetched: {monthDays.Count}");
        System.Diagnostics.Debug.WriteLine($"Workout days: {monthDays.Count(d => d.HasWorkout)}");
        foreach (var day in monthDays.Where(d => d.HasWorkout))
        {
            System.Diagnostics.Debug.WriteLine($"Workout Day: {day.Date:yyyy-MM-dd}, Completed: {day.IsWorkoutCompleted}");
        }

        DateTime firstDay = new DateTime(_currentDate.Year, _currentDate.Month, 1);
        int startDayOfWeek = (int)firstDay.DayOfWeek;
        int row = 0;
        int col = 0;

        for (int i = 0; i < startDayOfWeek; i++)
        {
            CalendarDays.Add(new CalendarDay
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
                    day.RemoveWorkoutCommand = new RelayCommand(
                        execute: () => RemoveWorkout(day),
                        canExecute: () => day.Date >= today // Ensures command is only executable for present/future
                    );

                    day.ChangeWorkoutCommand = new RelayCommand(
                        execute: () => ChangeWorkout(day),
                        canExecute: () => day.Date >= today
                    );
                }
                CalendarDays.Add(day);
            col++;
            if (col > 6) { col = 0; row++; }
        }

        OnPropertyChanged(nameof(CalendarDays));
        OnPropertyChanged(nameof(WorkoutDaysCountText)); // Notify UI of new property
    }

        private void RemoveWorkout(CalendarDay day)
        {
            var workout = _calendarRepository.GetUserWorkout(_userId, day.Date);
            if (workout != null)
            {
                DeleteUserWorkout(workout.WorkoutId, day.Date);
            }
        }

        private void ChangeWorkout(CalendarDay day)
        {
            var workout = _calendarRepository.GetUserWorkout(_userId, day.Date);
            if (workout != null)
            {
                DeleteUserWorkout(workout.WorkoutId, day.Date);
            }

        }

        private void PreviousMonth()
        {
            _currentDate = _currentDate.AddMonths(-1);
            UpdateCalendar();
        }

        private void NextMonth()
        {
            _currentDate = _currentDate.AddMonths(1);
            UpdateCalendar();
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
            UpdateCalendar();
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
            UpdateCalendar();
        }

        public void DeleteUserWorkout(int workoutId, DateTime date)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    DELETE FROM UserWorkouts 
                    WHERE UID = @UID AND WID = @WID AND Date = @Date";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", _userId);
                    cmd.Parameters.AddWithValue("@WID", workoutId);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    cmd.ExecuteNonQuery();
                }
            }
            UpdateCalendar();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // RelayCommand implementations remain unchanged
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);
    }
}