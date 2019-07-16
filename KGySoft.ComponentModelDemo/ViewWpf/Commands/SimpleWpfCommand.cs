#region Usings

using System;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Commands
{
    // The equivalent of KGySoft.ComponentModel.SimpleCommand for Microsoft's ICommand.
    // The term "Wpf" in the name just denotes that it is an <see cref="System.Windows.Input.ICommand"/> and not a <see cref="KGySoft.ComponentModel.ICommand"/>.
    // See also ParameterizedWpfCommand and KGyCommandAdapter.
    /// <summary>
    /// A simple "relay" command from a delegate with a switchable <see cref="SwitchableWpfCommandBase.CanExecute"/> property.
    /// </summary>
    public class SimpleWpfCommand : SwitchableWpfCommandBase
    {
        #region Fields

        private readonly Action callback;
        private readonly Func<bool> canExecute;

        #endregion

        #region Constructors

        public SimpleWpfCommand(Action callback, Func<bool> canExecute = null)
        {
            this.callback = callback;
            this.canExecute = canExecute;
        }

        #endregion

        #region Methods

        #region Public Methods

        public override void Execute(object _) => callback.Invoke();

        #endregion

        #region Protected Methods

        protected override bool EvaluateCanExecute(object _)
        {
            if (canExecute != null)
                CanExecute = canExecute.Invoke();
            return CanExecute;
        }

        #endregion

        #endregion
    }
}
