using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFMVVMUtility
{
    /// <summary>
    /// A property to be used in WPF Viewmodels. Always use "SomeProperty.Value" for xaml bindings.
    /// The bindings will only work properly if the property is initialized in the constructor of the viewmodel (the value can be set afterwards).
    /// </summary>
    /// <typeparam name="T">The type of the Value property</typeparam>
    public class NotifyingProperty<T> : INotifyPropertyChanged
    {
        public NotifyingProperty()
        {

        }

        /// <summary>
        /// Initalizes the property with the provided value. This will not cause a <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="initialValue">the initial value of <see cref="Value"/></param>
        public NotifyingProperty(T initialValue)
        {
            _value = initialValue;
        }

        /// <summary>
        /// not intended for normal use. use with caution
        /// </summary>
        /// <param name="value"></param>
        protected void SetValueWithoutUpdate(T value)
        {
            _value = value;
        }

        private T _value;

        /// <summary>
        /// The value of the property used to bind from xaml. Changing this property will cause a
        /// <see cref="ValueChanged"/> and <see cref="PropertyChanged"/> event.
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                if (ReferenceEquals(_value, value)) return;
                if (_value is IEquatable<T> && _value.Equals(value)) return;

                _value = value;
                RaiseValueChanged();
            }
        }

        /// <summary>
        /// Used as hook for WPF observable bindings
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;

        /// <summary>
        /// Raises the <see cref="ValueChanged"/> event.
        /// Also calls <see cref="RaisePropertyChanged"/> afterwards.
        /// </summary>
        protected virtual void RaiseValueChanged()
        {
            var handler = ValueChanged;
            handler?.Invoke(this, new ValueChangedEventArgs<T>(this.Value));
            //raise property changed for Value
            RaisePropertyChanged(nameof(Value));
        }

        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyname = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
