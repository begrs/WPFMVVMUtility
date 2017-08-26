using System;
using System.Windows.Input;

namespace WPFMVVMUtility
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _executedHandler;
        private readonly Func<object, bool> _canExecuteHandler;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _executedHandler = execute;
            _canExecuteHandler = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null)
                return true;
            return _canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            _executedHandler(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
