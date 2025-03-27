using System;
using System.Windows.Input;

namespace NeoIsisJob.Commands
{
    //used for handling commands
    public class RelayCommand : ICommand
    {
        //the action that should happen when the command is executed
        private readonly Action _execute;
        //optional function -> determines if the comm can be executed, if null -> always executable
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();

        //notifies the ui that can execute changed
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

}
