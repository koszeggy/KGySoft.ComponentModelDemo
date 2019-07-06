using System.ComponentModel;
using System.Windows.Forms;
using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.CoreLibraries;

namespace KGySoft.ComponentModelDemo.ViewWinForms
{
    public partial class EditMenuStrip : ToolStrip
    {
        private readonly CommandBindingsCollection commandBindings = new WinformsCommandBindingsCollection();
        private readonly ToolStripButton btnUndo = new ToolStripButton();
        private readonly ToolStripButton btnRedo = new ToolStripButton();
        private readonly ToolStripButton btnBeginEdit = new ToolStripButton();
        private readonly ToolStripButton btnEndEdit = new ToolStripButton();
        private readonly ToolStripButton btnCancelEdit = new ToolStripButton();

        private readonly ICommandState undoState = new CommandState
        {
            { nameof(ToolStripButton.Image), Images.Undo },
            { nameof(ToolStripButton.ToolTipText), "Undo last change" }
        };

        private readonly ICommandState redoState = new CommandState
        {
            { nameof(ToolStripButton.Image), Images.Redo },
            { nameof(ToolStripButton.ToolTipText), "Redo last change" }
        };

        private readonly ICommandState beginEditState = new CommandState
        {
            { nameof(ToolStripButton.Image), Images.Edit },
            { nameof(ToolStripButton.ToolTipText), "Begin New Edit" }
        };

        private readonly ICommandState endEditState = new CommandState
        {
            { nameof(ToolStripButton.Image), Images.Accept },
            { nameof(ToolStripButton.ToolTipText), "Commit Last Edit" }
        };

        private readonly ICommandState cancelEditState = new CommandState
        {
            { nameof(ToolStripButton.Image), Images.Cancel },
            { nameof(ToolStripButton.ToolTipText), "Cancel Edit" }
        };

        private object dataSource;

        public EditMenuStrip()
        {
            InitializeComponent();

            // ReSharper disable once VirtualMemberCallInConstructor
            Items.AddRange(new ToolStripItem[] { btnUndo, btnRedo, btnBeginEdit, btnEndEdit, btnCancelEdit });
            ApplyDataSource();
        }

        public object DataSource
        {
            get => dataSource;
            set
            {
                if (value == dataSource)
                    return;
                dataSource = value;
                ApplyDataSource();
            }
        }

        private void ApplyDataSource()
        {
            commandBindings.Clear();
            ResetStates();
            commandBindings.Add(Commands.Undo, undoState).AddSource(btnUndo, nameof(btnUndo.Click));
            commandBindings.Add(Commands.Redo, redoState).AddSource(btnRedo, nameof(btnRedo.Click));
            commandBindings.Add(Commands.BeginEdit, beginEditState).AddSource(btnBeginEdit, nameof(btnBeginEdit.Click));
            commandBindings.Add(Commands.EndEdit, endEditState).AddSource(btnEndEdit, nameof(btnEndEdit.Click));
            commandBindings.Add(Commands.CancelEdit, cancelEditState).AddSource(btnCancelEdit, nameof(btnCancelEdit.Click));
            if (dataSource != null)
                commandBindings.ForEach(b => b.AddTarget(dataSource));
            if (dataSource is INotifyPropertyChanged)
                commandBindings.Add<PropertyChangedEventArgs>(OnDataSourcePropertyChanged).AddSource(dataSource, nameof(INotifyPropertyChanged.PropertyChanged));
        }

        private void ResetStates()
        {
            ResetUndoState();
            ResetRedoState();
            ResetBeginEditState();
            ResetEndCancelEditState();
        }

        private void ResetUndoState() => undoState.Enabled = (dataSource as ICanUndo)?.CanUndo == true;
        private void ResetRedoState() => redoState.Enabled = (dataSource as ICanUndoRedo)?.CanRedo == true;
        private void ResetBeginEditState() => beginEditState.Enabled = (dataSource is ICanEdit);
        private void ResetEndCancelEditState() => endEditState.Enabled = cancelEditState.Enabled = (dataSource as ICanEdit)?.EditLevel > 0;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                commandBindings.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnDataSourcePropertyChanged(ICommandSource<PropertyChangedEventArgs> source)
        {
            switch (source.EventArgs.PropertyName)
            {
                case nameof(ICanUndo.CanUndo):
                    ResetUndoState();
                    break;
                case nameof(ICanUndoRedo.CanRedo):
                    ResetRedoState();
                    break;
                case nameof(ICanEdit.EditLevel):
                    ResetEndCancelEditState();
                    break;
            }
        }
    }
}
