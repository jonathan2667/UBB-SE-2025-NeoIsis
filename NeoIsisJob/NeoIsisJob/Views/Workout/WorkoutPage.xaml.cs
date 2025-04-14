using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NeoIsisJob.ViewModels.Workout;
using NeoIsisJob.Models;
using NeoIsisJob.Views.Workout;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NeoIsisJob.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkoutPage : Page
    {
        private WorkoutViewModel _workoutViewModel;
        private WorkoutModel _selectedWorkoutForEdit;

        public WorkoutViewModel ViewModel { get; set; }

        public WorkoutPage()
        {
            this.InitializeComponent();
            ViewModel = new WorkoutViewModel();
            this.DataContext = ViewModel;
        }

        public void GoToMainPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        public void GoToWorkoutPage_Tap(object sender, RoutedEventArgs e)
        {
            // Already on WorkoutPage
        }

        public void GoToCalendarPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CalendarPage));
        }

        public void GoToClassPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassPage));
        }

        public void GoToRankingPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RankingPage));
        }

        public void GoToSelectedWorkoutPage_Click(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem is WorkoutModel selectedWorkout)
            {
                SelectedWorkoutViewModel selectedWorkoutViewModel = App.Services.GetService<SelectedWorkoutViewModel>();
                selectedWorkoutViewModel.SelectedWorkout = selectedWorkout;
                this.Frame.Navigate(typeof(SelectedWorkoutPage));
            }
        }

        public void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateWorkoutPage));
        }

        private void WorkoutTypeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is WorkoutTypeModel selectedType)
            {
                ViewModel.ApplyWorkoutTypeFilter(selectedType, checkBox.IsChecked == true);
            }
        }

        private void EditWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WorkoutModel workout)
            {
                if (DataContext is WorkoutViewModel viewModel)
                {
                    viewModel.SelectedWorkout = workout;
                    WorkoutNameTextBox.Text = workout.Name;
                    EditWorkoutPopup.IsOpen = true;
                }
            }
        }
    }
}