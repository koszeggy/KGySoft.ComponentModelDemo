#region Usings

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.ViewWpf.Commands;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Controls
{
    // See also the ViewWinForms.Controls.EditMenuStrip control for another solution for Windows Forms.
    /// <summary>
    /// A specialized <see cref="ToolBar"/> control that demonstrates KGy SOFT <see cref="ICanUndoRedo"/> and <see cref="ICanEdit"/> features
    /// using KGy SOFT commands. Just set the <see cref="FrameworkElement.DataContext"/> property to an undoable or editable object.
    /// </summary>
    public partial class EditToolBar
    {
        #region Properties

        // We wrap the KGy SOFT commands into regular "WPF commands" here by the KGyCommandAdapter class.
        // Similarly to MainWindow, we could have used the EventToKGyCommand extension for the Click event of the buttons in the XAML.
        // In that case we would not need these properties; however, we would need to define ICommandState instances for adjusting the enabled states.
        // See also the MainWindow for EventToKGyCommand examples.
        public KGyCommandAdapter UndoCommand { get; } = new KGyCommandAdapter(Model.Commands.Undo);
        public KGyCommandAdapter RedoCommand { get; } = new KGyCommandAdapter(Model.Commands.Redo);
        public KGyCommandAdapter BeginEditCommand { get; } = new KGyCommandAdapter(Model.Commands.BeginEdit);
        public KGyCommandAdapter EndEditCommand { get; } = new KGyCommandAdapter(Model.Commands.EndEdit);
        public KGyCommandAdapter CancelEditCommand { get; } = new KGyCommandAdapter(Model.Commands.CancelEdit);

        #endregion

        #region Constructors

        public EditToolBar()
        {
            InitializeComponent();
            DataContextChanged += EditToolBar_DataContextChanged;
            ResetStates();
        }

        #endregion

        #region Methods

        #region Private Methods

        private void ResetStates()
        {
            ResetUndoState();
            ResetRedoState();
            ResetBeginEditState();
            ResetEndCancelEditState();
        }

        private void ResetUndoState() => UndoCommand.CanExecute = (DataContext as ICanUndo)?.CanUndo == true;
        private void ResetRedoState() => RedoCommand.CanExecute = (DataContext as ICanUndoRedo)?.CanRedo == true;
        private void ResetBeginEditState() => BeginEditCommand.CanExecute = (DataContext is ICanEdit);
        private void ResetEndCancelEditState() => EndEditCommand.CanExecute = CancelEditCommand.CanExecute = (DataContext as ICanEdit)?.EditLevel > 0;

        #endregion

        #region Event handlers

        private void EditToolBar_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged notifyPropertyChanged)
                notifyPropertyChanged.PropertyChanged -= NotifyPropertyChanged_PropertyChanged;
            ResetStates();
            notifyPropertyChanged = e.NewValue as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
                notifyPropertyChanged.PropertyChanged += NotifyPropertyChanged_PropertyChanged;
        }

        private void NotifyPropertyChanged_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
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
