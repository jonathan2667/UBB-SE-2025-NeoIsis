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
using NeoIsisJob.Services;

namespace NeoIsisJob.ViewModels.Calendar
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTime _currentDate;
        private string _yearText;
        private string _monthText;
        private ObservableCollection<CalendarDay> _calendarDays;
        private readonly ICalendarService _calendarService;
        public readonly int _userId;
        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarViewModel(int userId, ICalendarService calendarService = null)
        {
            _userId = userId;
            _currentDate = DateTime.Now;
            _calendarService = calendarService ?? new CalendarService();
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
                OnPropertyChanged(nameof(WorkoutDaysCountText));
                OnPropertyChanged(nameof(DaysCountText));
            }
        }

        public string WorkoutDaysCountText => _calendarService.GetWorkoutDaysCountText(CalendarDays);
        public string DaysCountText => _calendarService.GetDaysCountText(CalendarDays);
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public void UpdateCalendar()
        {
            YearText = _currentDate.Year.ToString();
            MonthText = _currentDate.ToString("MMMM");
            CalendarDays = _calendarService.GetCalendarDays(_userId, _currentDate);
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
            _calendarService.AddUserWorkout(userWorkout);
            UpdateCalendar();
        }

        public void UpdateUserWorkout(UserWorkoutModel userWorkout)
        {
            _calendarService.UpdateUserWorkout(userWorkout);
            UpdateCalendar();
        }

        public void DeleteUserWorkout(int workoutId, DateTime date)
        {
            _calendarService.DeleteUserWorkout(_userId, workoutId, date);
            UpdateCalendar();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

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
