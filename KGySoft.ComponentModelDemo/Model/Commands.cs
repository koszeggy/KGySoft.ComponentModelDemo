#region Usings

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.Model
{
    /// <summary>
    /// Contains commands for <see cref="ICanUndoRedo"/> and <see cref="ICanEdit"/> objects.
    /// </summary>
    public static class Commands
    {
        #region Properties

        // Usually commands are recommended to be static members. These class contains such general commands.
        // These are all targeted commands (similar to parameterized ones but the command is executed once for each targets).
        // See further examples in ViewModel.MainViewModel and in ViewWpf.Windows.MainWindow
        public static ICommand Undo { get; } = new TargetedCommand<ICanUndo>(OnUndo);
        public static ICommand Redo { get; } = new TargetedCommand<ICanUndoRedo>(OnRedo);
        public static ICommand BeginEdit { get; } = new TargetedCommand<ICanEdit>(OnBeginEdit);
        public static ICommand EndEdit { get; } = new TargetedCommand<ICanEdit>(OnEndEdit);
        public static ICommand CancelEdit { get; } = new TargetedCommand<ICanEdit>(OnCancelEdit);

        #endregion

        #region Methods

        private static void OnUndo(ICanUndo target)
        {
            if (!target.CanUndo)
                return;
            target.TryUndo();
        }

        private static void OnRedo(ICanUndoRedo target)
        {
            if (!target.CanRedo)
                return;
            target.TryRedo();
        }

        private static void OnBeginEdit(ICanEdit target) => target.BeginNewEdit();

        private static void OnEndEdit(ICanEdit target)
        {
            if (target.EditLevel < 1)
                return;
            target.CommitLastEdit();
        }

        private static void OnCancelEdit(ICanEdit target)
        {
            if (target.EditLevel < 1)
                return;
            target.RevertLastEdit();
        }

        #endregion
    }
}
