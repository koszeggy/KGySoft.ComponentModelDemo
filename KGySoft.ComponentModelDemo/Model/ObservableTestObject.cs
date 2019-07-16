#region Usings

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.Model
{
    // A model class that implements INotifyPropertyChanged.
    public class ObservableTestObject : ObservableObjectBase, ITestObject
    {
        #region Properties

        public int IntProp { get => Get<int>(); set => Set(value); }
        public string StringProp { get => Get<string>(); set => Set(value); }

        #endregion
    }
}
