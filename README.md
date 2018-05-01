# WPFMVVMUtility
WPF MVVM Utility Properties

Please view the provided WPF Test App for an example implementation.
The basic idea is to have an easy way to use MVVM in small WPF apps/tools.
It omits the usual 

```cs
       public string SomeName
        {
            get
            {
                return _somename
            }
            set
            {
                if (value == _somename) return;
                _somename = SomeName;
                RaiseChangedEvent(SomeName);
            }
        }
```
and also provides some utility functions for value converters for bindings.
The ValidatingNotifyingProperty provides easy to use validating properties that support multiple error messages.


Example Property: 
   ```cs
        public NotifyingProperty<string> TextNotifyingProperty { get; } = new NotifyingProperty<string>(); 
   ```
Binding in xaml:
   ```xml
   <TextBox Text="{Binding TextNotifyingProperty.Value}" />
   ```
   Done :)
   
   
   Feel free to submit suggestions, issues or pullrequest.
   
   
   Distant Future:
   
    * Autocomplete Textbox with optional automatic injection of recently used terms.
    * Full UnitTest coverage.
