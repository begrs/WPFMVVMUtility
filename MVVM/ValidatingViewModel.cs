using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace WPFMVVMUtility
{
    [Obsolete("use ValidatingNotifyingProperty for individual properties instead")]
    public abstract class ValidatingViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors;

        protected ValidatingViewModel()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        protected void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public void AddError(string message, [CallerMemberName]string property = "")
        {
            if (_errors.ContainsKey(property))
                _errors[property].Add(message);
            else
                _errors.Add(property, new List<string>() { message });
            RaiseErrorsChanged(property);
        }

        public void RemoveErrors([CallerMemberName]string property = "")
        {
            if (_errors.ContainsKey(property))
                _errors.Remove(property);
            RaiseErrorsChanged(property);
        }

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : new List<string>();
        }
    }
}
