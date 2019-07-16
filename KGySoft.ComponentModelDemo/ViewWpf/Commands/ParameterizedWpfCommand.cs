#region Usings

using System;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Commands
{
    // The equivalent of KGySoft.ComponentModel.ParameterizedCommand for Microsoft's ICommand.
    // The term "Wpf" in the name just denotes that it is an <see cref="System.Windows.Input.ICommand"/> and not a <see cref="KGySoft.ComponentModel.ICommand"/>.
    // See also SimpleWpfCommand and KGyCommandAdapter.
    /// <summary>
    /// A parameterized "relay" command from a delegate with a switchable <see cref="SwitchableWpfCommandBase.CanExecute"/> property.
    /// </summary>
    public class ParameterizedWpfCommand<T> : SwitchableWpfCommandBase
    {
        #region Fields

        private readonly Action<T> callback;
        private readonly Func<T, bool> canExecute;

        #endregion

        #region Constructors

        public ParameterizedWpfCommand(Action<T> callback, Func<T, bool> canExecute = null)
        {
            this.callback = callback;
            this.canExecute = canExecute;
        }

        #endregion

        #region Methods

        #region Public Methods

        public override void Execute(object parameter) => callback.Invoke((T)parameter);

        #endregion

        #region Protected Methods

        protected override bool EvaluateCanExecute(object parameter)
        {
            if (canExecute != null)
                CanExecute = canExecute.Invoke((T)parameter);
            return CanExecute;
        }

        #endregion

        #endregion
    }
}
