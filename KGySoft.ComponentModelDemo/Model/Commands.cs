using KGySoft.ComponentModel;

namespace KGySoft.ComponentModelDemo.Model
{
    internal static class Commands
    {
        internal static ICommand Undo { get; } = new TargetedCommand<ICanUndo>(OnUndo);
        internal static ICommand Redo { get; } = new TargetedCommand<ICanUndoRedo>(OnRedo);
        internal static ICommand BeginEdit { get; } = new TargetedCommand<ICanEdit>(OnBeginEdit);
        internal static ICommand EndEdit { get; } = new TargetedCommand<ICanEdit>(OnEndEdit);
        internal static ICommand CancelEdit { get; } = new TargetedCommand<ICanEdit>(OnCancelEdit);

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
    }
}
