using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFMVVMUtility;

namespace MVVMTest
{
    [TestClass]
    public class NotifyingPropertyTest
    {
        [TestMethod]
        public void NotifyingPropety_ctr_ShouldUseInitialValue()
        {
            const string initial = "initial";
            var property = new NotifyingProperty<string>(initial);
            Assert.AreEqual(initial, property.Value);
        }

        [TestMethod]
        public void NotifyingPropet_Valuey_ShouldRaiseValueChangedEvent_IfModified()
        {
            const string initial = "initial";
            var raised = false;
            var property = new NotifyingProperty<string>(initial);
            property.ValueChanged += (sender, e) => raised = true;

            property.Value = "test";
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void NotifyingPropety_Value_ShouldRaiseValueChangedEvent_IfPreviouslyUnsetValueIsSet()
        {
            var raised = false;
            var property = new NotifyingProperty<string>();
            property.ValueChanged += (sender, e) => raised = true;

            property.Value = "test";
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void NotifyingPropety_Value_ShouldNotRaiseValueChangedEvent_IfNewValueIsEqualToOld_Null()
        {
            var raised = false;
            var property = new NotifyingProperty<string>(null);
            property.ValueChanged += (sender, e) => raised = true;
            property.Value = null;
            Assert.IsFalse(raised);
        }

        [TestMethod]
        public void NotifyingPropety_Value_ShouldNotRaiseValueChangedEvent_IfNewValueIsEqualToOld_NotNull()
        {
            var raised = false;
            var property = new NotifyingProperty<string>("test2");
            property.ValueChanged += (sender, e) => raised = true;
            property.Value = "test2";
            Assert.IsFalse(raised);
        }

        [TestMethod]
        public void NotifyingPropety_Value_ShouldRaisePropertyChangedAfterValueChangedEvent()
        {
            const string initial = "initial";
            var raised = 1;
            var property = new NotifyingProperty<string>(initial);
            // different values of raised will show which one was exceuted first.
            property.ValueChanged += (sender, e) => raised *= 2;
            property.PropertyChanged += (sender, e) => raised += 3;

            property.Value = "test";
            Assert.AreEqual(5 ,raised);
        }
    }
}
