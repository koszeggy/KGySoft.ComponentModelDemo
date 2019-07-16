#region Usings

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.Model
{
    // A model class that implements ICanEdit (KGySoft) and IEditableObject (Microsoft) interfaces.
    // IEditableObject is supported by several UI environments (eg. grid controls of WPF and WinForms).
    // For ICanEdit demo see the Commands class as well as the ViewWpf.Controls.EditToolBar and ViewWinForms.Controls.EditMenuStrip controls.
    public class EditableTestObject : EditableObjectBase, ITestObject
    {
        #region Properties

        public int IntProp { get => Get<int>(); set => Set(value); }
        public string StringProp { get => Get<string>(); set => Set(value); }

        #endregion
    }
}
