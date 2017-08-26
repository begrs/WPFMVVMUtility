using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFMVVMUtility;

namespace MVVMTest
{
    [TestClass]
    public class ValidatingNotifyingPropertyTest
    {
        const string Initial = "initial";

        [TestMethod]
        public void ValidatingNotifyingProperty_ctr_IntitalValueShouldCauseValidationIfParameterIsTrue()
        {
            var property = new ValidatingNotifyingProperty<string>(Initial, true,
                new Dictionary<string, Func<string, bool>>
                {
                    {"Implicit test if initial value was set correctly. Always returns error = true", value => value == Initial }
                });
            Assert.IsTrue(property.HasErrors);
        }

        [TestMethod]
        public void ValidatingNotifyingProperty_ctr_IntitalValueShouldNotCauseValidationIfParameterIsFalse()
        {
            var property = new ValidatingNotifyingProperty<string>(Initial, false,
                new Dictionary<string, Func<string, bool>>
                {
                    {"Always returns error = true", value => value == Initial }
                });
            Assert.IsFalse(property.HasErrors);
        }

        [TestMethod]
        public void ValidatingNotifyingProperty_ErrorsChanged_ShouldRaiseErrorsChangedIfNewError()
        {
            var property = new ValidatingNotifyingProperty<string>(Initial, true,
                new Dictionary<string, Func<string, bool>>
                {
                    {"The value is now the new value. How dare you!", value => value != Initial }
                });
            var raised = false;
            property.ErrorsChanged += (s, e) => raised = true;
            property.Value = "new value";
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void ValidatingNotifyingProperty_ErrorsChanged_ShouldRaiseErrorsChangedIfSameErrorsButValueChanged_NoErrors()
        {
            const string newvalue = "new value";
            var property = new ValidatingNotifyingProperty<string>(Initial, true,
                new Dictionary<string, Func<string, bool>>
                {
                    {"The value is not exactly like i want it to be.", value => value != Initial && value != newvalue}
                });
            Assert.IsFalse(property.HasErrors);

            var raised = false;
            property.ErrorsChanged += (s, e) => raised = true;
            property.Value = newvalue;
            Assert.IsFalse(property.HasErrors);
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void ValidatingNotifyingProperty_ErrorsChanged_ShouldRaiseErrorsChangedIfSameErrorsButValueChanged_SomeErrors()
        {
            var property = new ValidatingNotifyingProperty<string>("not initial", true,
                new Dictionary<string, Func<string, bool>>
                {
                    {"The value is now the new value. How dare you!", value => value != Initial }
                });
            Assert.IsTrue(property.HasErrors);
            var raised = false;
            property.ErrorsChanged += (s, e) => raised = true;
            property.Value = "new value";
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void ValidatingNotifyingProperty_AddValidationError_ShouldProvideValueToErrorMessage()
        {
            const string errorMessage = "The value {0} is not welcome here.";

            var property = new ValidatingNotifyingProperty<string>(Initial, true,
                new Dictionary<string, Func<string, bool>>
                {
                    {errorMessage, value => true }
                });

            Assert.AreEqual(string.Format(errorMessage, Initial), property.Errors[0]);
        }
    }
}
