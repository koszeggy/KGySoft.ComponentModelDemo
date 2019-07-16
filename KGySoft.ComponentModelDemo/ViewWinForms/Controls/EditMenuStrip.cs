#region Usings

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.ViewWinForms.Commands;
using KGySoft.CoreLibraries;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWinForms.Controls
{
    // See also the ViewWpf.Controls.EditToolBar control for another solution for WPF.
    /// <summary>
    /// A specialized <see cref="ToolStrip"/> control that demonstrates KGy SOFT <see cref="ICanUndoRedo"/> and <see cref="ICanEdit"/> features
    /// using KGy SOFT commands. Just bind the <see cref="DataSource"/> property to an undoable or editable object.
    /// </summary>
    public class EditMenuStrip : ToolStrip
    {
        #region Fields

        // In this class every command binding has a Control source so we use the specialized WinformsCommandBindingsCollection,
        // which visualizes every command state property on the source (Enabled, Image, ToolTipText).
        private readonly CommandBindingsCollection commandBindings = new WinformsCommandBindingsCollection();

        // Note that we don't set initialize any properties of the buttons. The command state will provide the needed properties.
        private readonly ToolStripButton btnUndo = new ToolStripButton();
        private readonly ToolStripButton btnRedo = new ToolStripButton();
        private readonly ToolStripButton btnBeginEdit = new ToolStripButton();
        private readonly ToolStripButton btnEndEdit = new ToolStripButton();
        private readonly ToolStripButton btnCancelEdit = new ToolStripButton();

        // We keep references to the states of the commands so we can toggle their Enabled state (true by default). We add some further state properties.
        // As the used WinformsCommandBindingsCollection adds the a PropertyCommandStateUpdater to each binding, the Enabled state will be reflected by the buttons, too.
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

        // Keeping a reference to the DataSource.PropertyChanged binding because this one is removed and added dynamically
        private ICommandBinding sourceChangedBinding;

        private object dataSource;

        #endregion

        #region Properties

        // Unlike in ValidationResultToErrorProviderAdapter, we don't support collection sources here.
        // So the binding for this property has to be set just like a binding for a TextBox, for example.
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

        #endregion

        #region Constructors

        public EditMenuStrip()
        {
            // Just adjusting DPI, which is not supported automatically by ToolStrip
            double scale;
            using (Graphics g = CreateGraphics())
                scale = Math.Round(Math.Max(g.DpiX, g.DpiY) / 96, 2);
            if (scale > 1)
            {
                ImageScalingSize = new Size((int)(ImageScalingSize.Width * scale), (int)(ImageScalingSize.Height * scale));
                AutoSize = false;
            }

            // ReSharper disable once VirtualMemberCallInConstructor
            Items.AddRange(new ToolStripItem[] { btnUndo, btnRedo, btnBeginEdit, btnEndEdit, btnCancelEdit });

            // binding the Click event of the buttons to the various Model.Commands commands
            commandBindings.Add(Model.Commands.Undo, undoState).AddSource(btnUndo, nameof(btnUndo.Click));
            commandBindings.Add(Model.Commands.Redo, redoState).AddSource(btnRedo, nameof(btnRedo.Click));
            commandBindings.Add(Model.Commands.BeginEdit, beginEditState).AddSource(btnBeginEdit, nameof(btnBeginEdit.Click));
            commandBindings.Add(Model.Commands.EndEdit, endEditState).AddSource(btnEndEdit, nameof(btnEndEdit.Click));
            commandBindings.Add(Model.Commands.CancelEdit, cancelEditState).AddSource(btnCancelEdit, nameof(btnCancelEdit.Click));
            commandBindings.ForEach(b => b.AddTarget(() => DataSource)); // their target will be the current DataSource in the moment of the invocation

            ApplyDataSource();
        }

        #endregion

        #region Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                commandBindings.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods

        private void ApplyDataSource()
        {
            if (sourceChangedBinding != null)
                commandBindings.Remove(sourceChangedBinding);

            ResetStates();

            if (dataSource is INotifyPropertyChanged)
                sourceChangedBinding = commandBindings.Add<PropertyChangedEventArgs>(OnDataSourcePropertyChangedCommand).AddSource(dataSource, nameof(INotifyPropertyChanged.PropertyChanged));
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

        #endregion

        #region Command Handlers

        private void OnDataSourcePropertyChangedCommand(ICommandSource<PropertyChangedEventArgs> source)
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

        #endregion

        #endregion
    }
}
