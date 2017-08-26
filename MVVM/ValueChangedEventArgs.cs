namespace WPFMVVMUtility
{
    public class ValueChangedEventArgs<T>
    {
        public ValueChangedEventArgs(T newValue)
        {
            NewValue = newValue;
        }

        public T NewValue { get; }
    }
}
