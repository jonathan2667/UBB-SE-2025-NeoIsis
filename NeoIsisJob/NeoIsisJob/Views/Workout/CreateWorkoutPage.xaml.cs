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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NeoIsisJob.Views.Workout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateWorkoutPage : Page
    {
        private CreateWorkoutViewModel _viewModel;

        public CreateWorkoutViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public CreateWorkoutPage()
        {
            //bind the view with the view model
            this.InitializeComponent();
            this.ViewModel = new CreateWorkoutViewModel();
            this.DataContext = this.ViewModel;
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //reset the changes
            //ResetFormFields();

            //and now go back
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void ResetFormFields()
        {
            ////reset the workout name text box
            //this.WorkoutNameTextBox.Text = string.Empty;

            ////reset the combobox to no selection
            //this.WorkoutTypeComboBox.SelectedIndex = -1;

            //TODO -> handle exercises selection reset
        }
    }
}
