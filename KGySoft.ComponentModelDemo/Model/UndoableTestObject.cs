#region Usings

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.Model
{
    // A model class that implements ICanUndoRedo (KGySoft) and IRevertibleChangeTracking (Microsoft) interfaces.
    // For ICanUndoRedo demo see the Commands class as well as the ViewWpf.Controls.EditToolBar and ViewWinForms.Controls.EditMenuStrip controls.
    public class UndoableTestObject : UndoableObjectBase, ITestObject
    {
        #region Properties

        public int IntProp { get => Get<int>(); set => Set(value); }
        public string StringProp { get => Get<string>(); set => Set(value); }

        #endregion
    }
}
