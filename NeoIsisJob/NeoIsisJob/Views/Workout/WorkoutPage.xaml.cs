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
            //this.Frame.Navigate(typeof(WorkoutPage));
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
                //get the singleton registered in app and set its workout to the selected one
                SelectedWorkoutViewModel selectedWorkoutViewModel = App.Services.GetService<SelectedWorkoutViewModel>();
                selectedWorkoutViewModel.SelectedWorkout = selectedWorkout;

                //now navigate to the selected workout page
                this.Frame.Navigate(typeof(SelectedWorkoutPage));
            }
        }

        //handler for create button -> go to create workout page
        public void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //for now
            this.Frame.Navigate (typeof(CreateWorkoutPage));
        }

        //handler for checking a box
        private void WorkoutTypeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            //if the sender is a checkbox and its data is a workout type
            if (sender is CheckBox checkBox && checkBox.DataContext is WorkoutTypeModel selectedType)
            {
                //var viewModel = DataContext as WorkoutViewModel;
                if (this.ViewModel == null) return;

                //if checked, filter workouts and disable other checkboxes
                if (checkBox.IsChecked == true)
                {
                    //this triggers the list of workouts to change
                    this.ViewModel.SelectedWorkoutType = selectedType;
                    DisableOtherCheckBoxes(selectedType);
                }
                else
                {
                    //reset the filter
                    this.ViewModel.SelectedWorkoutType = null;
                    EnableAllCheckBoxes();
                }
            }
        }

        //disable all checkboxes except the selected one
        private void DisableOtherCheckBoxes(WorkoutTypeModel selectedType)
        {
            foreach (CheckBox checkBox in FindVisualChildren<CheckBox>(this))
            {
                if (checkBox.DataContext is WorkoutTypeModel type && type != selectedType)
                {
                    checkBox.IsEnabled = false;
                }
            }
        }

        //re-enable all checkboxes when filter is removed
        private void EnableAllCheckBoxes()
        {
            foreach (CheckBox checkBox in FindVisualChildren<CheckBox>(this))
            {
                checkBox.IsEnabled = true;
            }
        }

        //finds all the checkboxes in the visual tree
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T childItem) yield return childItem;

                foreach (var childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }

        private void EditWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WorkoutModel workout)
            {
                if (DataContext is WorkoutViewModel viewModel)
                {
                    // Set the selected workout in the ViewModel
                    viewModel.SelectedWorkout = workout;

                    // Pre-fill the TextBox with the current workout name
                    WorkoutNameTextBox.Text = workout.Name;

                    // Open the popup
                    EditWorkoutPopup.IsOpen = true;
                }
            }
        }

        private void UpdateWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newName = WorkoutNameTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(newName))
                {
                    if (DataContext is WorkoutViewModel viewModel)
                    {
                        viewModel.UpdateWorkoutName(newName);
                    }

                    // Close the popup
                    EditWorkoutPopup.IsOpen = false;
                }
                else
                {
                    throw new InvalidOperationException("Workout name cannot be empty or null.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating workout name.", ex);
            }
        }

        private void CancelEditWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup without making changes
            EditWorkoutPopup.IsOpen = false;
        }

        private void DeleteWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure the sender is a Button
            if (sender is Button button && button.Tag is int workoutId)
            {
                Debug.WriteLine($"Delete button clicked for Workout ID: {workoutId}");

                // Access the ViewModel
                if (DataContext is WorkoutViewModel viewModel)
                {
                    // Call the delete method in the ViewModel
                    viewModel.DeleteWorkout(workoutId);
                }
                else
                {
                    Debug.WriteLine("ViewModel is null.");
                }
            }
            else
            {
                Debug.WriteLine("Sender is not a Button or Tag is not an integer.");
            }
        }
    }
}