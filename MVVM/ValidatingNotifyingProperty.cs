using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WPFMVVMUtility
{
    /// <summary>
    /// Validations should return true if there is an invalid value.
    /// Remember to set "ValidatesOnDataErrors=true" on the binding.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidatingNotifyingProperty<T> : NotifyingProperty<T>, INotifyDataErrorInfo
    {
        private Dictionary<string, Func<T, bool>> _validations;

        /// <summary>
        /// Validations should return true if there is an invalid value.
        /// Validation message may containt {0} for the current value.
        /// </summary>
        public ValidatingNotifyingProperty(bool initialvalidation, IDictionary<string, Func<T, bool>> validations)
            : this(default(T), initialvalidation, validations)
        {

        }

        /// <summary>
        /// Validations should return true if there is an invalid value.
        /// Validation message may containt {0} for the current value.
        /// </summary>
        public ValidatingNotifyingProperty(T initialValue, bool initialvalidation, IDictionary<string, Func<T, bool>> validations)
        {
            _validations = new Dictionary<string, Func<T, bool>>(validations);
            if (initialvalidation)
            {
                ValueChanged += Validate;
                Value = initialValue;
            }
            else
            {
                SetValueWithoutUpdate(initialValue);
                ValueChanged += Validate;
            }
        }

        /// <summary>
        /// Validations should return true if there is an invalid value.
        /// </summary>
        /// <param name="test">Be sure to not throw exceptions here.</param>
        /// <param name="message">May contain {0} for current value.</param>
        public void AddValidationError(Func<T, bool> test, string message)
        {
            if (test == null) throw new ArgumentNullException(nameof(test));
            if (_validations == null)
            {
                _validations = new Dictionary<string, Func<T, bool>>();
                ValueChanged += Validate;
            }

            _validations.Add(message, test);
        }

        private void Validate(object sender, ValueChangedEventArgs<T> eventArgs)
        {
            Errors = _validations.Where(v => v.Value.Invoke(eventArgs.NewValue)).Select(
                v => string.Format(v.Key, eventArgs.NewValue)
                ).ToList();
            RaiseValueErrorsChanged();
        }

        private List<string> _errors;
        public List<string> Errors
        {
            get => _errors ?? (_errors = new List<string>());
            private set
            {
                if (value == _errors) return;
                _errors = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName == nameof(Value) ? Errors : new List<string>();
        }

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected virtual void RaiseValueErrorsChanged()
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Value)));
        }
    }
}
