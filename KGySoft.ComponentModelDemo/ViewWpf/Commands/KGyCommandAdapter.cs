#region Usings

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Commands
{
    // You can use this wrapper to expose KGy SOFT commands as Microsoft commands.
    // See the EditToolBar control for examples.
    // Alternatively you can use the EventToKGyCommand markup extension to bind events directly to KGy SOFT commands from XAML. See the MainWindow.xaml for examples.
    /// <summary>
    /// An <see cref="System.Windows.Input.ICommand"/> that wraps a <see cref="KGySoft.ComponentModel.ICommand"/>.
    /// </summary>
    public class KGyCommandAdapter : SwitchableWpfCommandBase
    {
        #region Fields

        private readonly ICommand wrappedCommand;
        private readonly ICommandState state;

        #endregion

        #region Constructors

        public KGyCommandAdapter(ICommand command, CommandState state = null)
        {
            wrappedCommand = command;
            if (state == null)
                return;

            // if a state was passed to the constructor, then we sync its Enabled state with CanExecute
            this.state = state;
            state.CreatePropertyBinding(nameof(state.Enabled), nameof(CanExecute), this);
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            // If a command state was passed to the constructor we double check its Enabled state
            // (though setting Enabled adjusts also CanExecute)
            if (state?.Enabled != false)
                wrappedCommand.Execute(null, state, parameter);
        }

        #endregion
    }
}
