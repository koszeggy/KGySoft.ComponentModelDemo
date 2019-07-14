using System.ComponentModel;
using System.Windows;
using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Model;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    /// <summary>
    /// Interaction logic for EditToolBar.xaml
    /// </summary>
    public partial class EditToolBar
    {
        // WPF part of commands must be instances due to CanExecute
        public KGyCommandAdapter UndoCommand { get; } = new KGyCommandAdapter(Commands.Undo);
        public KGyCommandAdapter RedoCommand { get; } = new KGyCommandAdapter(Commands.Redo);
        public KGyCommandAdapter BeginEditCommand { get; } = new KGyCommandAdapter(Commands.BeginEdit);
        public KGyCommandAdapter EndEditCommand { get; } = new KGyCommandAdapter(Commands.EndEdit);
        public KGyCommandAdapter CancelEditCommand { get; } = new KGyCommandAdapter(Commands.CancelEdit);

        public EditToolBar()
        {
            InitializeComponent();
            DataContextChanged += EditToolBar_DataContextChanged;
            ResetStates();
        }

        private void EditToolBar_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged notifyPropertyChanged)
                notifyPropertyChanged.PropertyChanged -= NotifyPropertyChanged_PropertyChanged;
            ResetStates();
            notifyPropertyChanged = e.NewValue as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
                notifyPropertyChanged.PropertyChanged += NotifyPropertyChanged_PropertyChanged;
        }

        private void ResetStates()
        {
            ResetUndoState();
            ResetRedoState();
            ResetBeginEditState();
            ResetEndCancelEditState();
        }

        private void ResetUndoState() => UndoCommand.IsEnabled = (DataContext as ICanUndo)?.CanUndo == true;
        private void ResetRedoState() => RedoCommand.IsEnabled = (DataContext as ICanUndoRedo)?.CanRedo == true;
        private void ResetBeginEditState() => BeginEditCommand.IsEnabled = (DataContext is ICanEdit);
        private void ResetEndCancelEditState() => EndEditCommand.IsEnabled = CancelEditCommand.IsEnabled = (DataContext as ICanEdit)?.EditLevel > 0;

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
    }
}
