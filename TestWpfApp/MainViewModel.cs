using System;
using System.Collections.Generic;
using WPFMVVMUtility;

namespace TestWpfApp
{
    /// <summary>
    /// Test usage of notifying properties
    /// </summary>
    public class MainViewModel
    {
        public MainViewModel()
        {
            TextNotifyingProperty = new NotifyingProperty<string>();
            TextValidatingNotifyingProperty = new ValidatingNotifyingProperty<string>("", true, new Dictionary<string, Func<string, bool>>
                {
                    { "Can not be empty", v => v.Length == 0},
                    { "Can not be more than six characters", v => v.Length > 6}
                });
        }

        public NotifyingProperty<string> TextNotifyingProperty { get; }

        public ValidatingNotifyingProperty<string> TextValidatingNotifyingProperty { get; }

    }
}
