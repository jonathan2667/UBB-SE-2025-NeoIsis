using NeoIsisJob.Models;
using NeoIsisJob.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;

namespace NeoIsisJob.ViewModels.Classes
{
    public class SelectedClassViewModel: INotifyPropertyChanged
    {
        private readonly ClassService _classService;
        private readonly UserClassService _userClassService;
        private ClassModel _selectedClass;
        private ObservableCollection<UserClassModel> _userClasses;

        public ClassModel SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                //signal that the property has changed
                Debug.WriteLine($"SelectedClass set to: {_selectedClass?.Name}"); // Debug message
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        //gets triggered every time a property changes
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
