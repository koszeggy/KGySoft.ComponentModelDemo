#region Usings

using System;
using System.Windows.Input;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Commands
{
    /// <summary>
    /// A base class for WPF commands with a switchable <see cref="CanExecute"/> property.
    /// The term "Wpf" in the name just denotes that it is an <see cref="System.Windows.Input.ICommand"/> and not a <see cref="KGySoft.ComponentModel.ICommand"/>.
    /// </summary>
    public abstract class SwitchableWpfCommandBase : ICommand
    {
        #region Fields

        private bool canExecute = true;

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Properties

        public bool CanExecute
        {
            get => canExecute;
            set
            {
                if (canExecute == value)
                    return;
                canExecute = value;
                OnCanExecuteChanged(EventArgs.Empty);
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        public abstract void Execute(object parameter);

        #endregion

        #region Protected Methods

        protected virtual bool EvaluateCanExecute(object parameter) => CanExecute;

        protected virtual void OnCanExecuteChanged(EventArgs e) => CanExecuteChanged?.Invoke(this, e);

        #endregion

        #region Explicitly Implemented Interface Methods

        bool ICommand.CanExecute(object parameter) => EvaluateCanExecute(parameter);

        #endregion

        #endregion
    }
}
