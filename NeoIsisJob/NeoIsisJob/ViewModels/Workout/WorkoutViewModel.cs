using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using NeoIsisJob.Commands;

namespace NeoIsisJob.ViewModels.Workout
{
    //INotifyPropertyChanged notifies the client that a property value has changed
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        private readonly WorkoutService _workoutService;
        private ObservableCollection<WorkoutModel> _workouts;

        //Workouts property for the list -> when set it, signal that it changed
        public ObservableCollection<WorkoutModel> Workouts
        {
            get => _workouts;
            set
            {
                _workouts = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadWorkoutsCommand { get; }

        public WorkoutViewModel()
        {
            this._workoutService = new WorkoutService();
            Workouts = new ObservableCollection<WorkoutModel>();

            // Command to load workouts
            //LoadWorkoutsCommand = new RelayCommand(LoadWorkouts);

            // Load workouts when viewmodel is created
            LoadWorkouts();
        }

        private void LoadWorkouts()
        {
            Workouts.Clear();

            //for debugging
            //var allWorkouts = _workoutService.GetAllWorkouts();

            //System.Diagnostics.Debug.WriteLine($"Workout count: {allWorkouts.Count}");

            foreach (var workout in this._workoutService.GetAllWorkouts())
            {
                Workouts.Add(workout);
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        //gets triggered every time a property changes
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
