using KGySoft.ComponentModel;

namespace BindingTest.Model
{
    public class ObservableTestObject : ObservableObjectBase, ITestObject
    {
        public int IntProp
        {
            get => Get<int>();
            set => Set(value);
        }

        public string StringProp
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
