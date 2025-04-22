﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using NeoIsisJob.Models;
using NeoIsisJob.Data;
using NeoIsisJob.Repositories;
using NeoIsisJob.Services;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.ViewModels.Calendar
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTime currentDate;
        private string yearText;
        private string monthText;
        private ObservableCollection<CalendarDay> calendarDays;

        private readonly ICalendarService calendarService;

        private readonly DatabaseHelper databaseHelper;

        public readonly int UserId;
        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarViewModel(int userId, ICalendarService calendarService = null)
        {
            this.UserId = userId;
            currentDate = DateTime.Now;

            this.calendarService = calendarService ?? new CalendarService();
            databaseHelper = new DatabaseHelper();
            CalendarDays = new ObservableCollection<CalendarDay>();
            PreviousMonthCommand = new RelayCommand(PreviousMonth);
            NextMonthCommand = new RelayCommand(NextMonth);
            UpdateCalendar();
        }

        public string YearText
        {
            get => yearText;
            set
            {
                yearText = value;
                OnPropertyChanged(nameof(YearText));
            }
        }

        public string MonthText
        {
            get => monthText;
            set
            {
                monthText = value;
                OnPropertyChanged(nameof(MonthText));
            }
        }

        public ObservableCollection<CalendarDay> CalendarDays
        {
            get => calendarDays;
            set
            {
                calendarDays = value;
                OnPropertyChanged(nameof(CalendarDays));
                OnPropertyChanged(nameof(WorkoutDaysCountText));
                OnPropertyChanged(nameof(DaysCountText));
            }
        }

        public string WorkoutDaysCountText => calendarService.GetWorkoutDaysCountText(CalendarDays);
        public string DaysCountText => calendarService.GetDaysCountText(CalendarDays);
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public void UpdateCalendar()
        {
            YearText = currentDate.Year.ToString();
            MonthText = currentDate.ToString("MMMM");
            CalendarDays = calendarService.GetCalendarDays(UserId, currentDate);
        }

        private void PreviousMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            UpdateCalendar();
        }

        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            UpdateCalendar();
        }

        public void AddUserWorkout(UserWorkoutModel userWorkout)
        {
            calendarService.AddUserWorkout(userWorkout);

            UpdateCalendar();
        }

        public void UpdateUserWorkout(UserWorkoutModel userWorkout)
        {
            calendarService.UpdateUserWorkout(userWorkout);
            UpdateCalendar();
        }

        public void DeleteUserWorkout(int workoutId, DateTime date)
        {
            calendarService.DeleteUserWorkout(UserId, workoutId, date);
            UpdateCalendar();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => canExecute == null || canExecute();

        public void Execute(object parameter) => execute();
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => canExecute == null || canExecute((T)parameter);

        public void Execute(object parameter) => execute((T)parameter);
    }
}
