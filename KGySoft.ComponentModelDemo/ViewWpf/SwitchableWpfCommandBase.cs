#region Usings

using System;
using System.Windows.Input;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    /// <summary>
    /// A base class for WPF commands with a switchable <see cref="IsEnabled"/> property.
    /// The term "Wpf" in the name just denotes that it is an <see cref="System.Windows.Input.ICommand"/> and not a <see cref="KGySoft.ComponentModel.ICommand"/>.
    /// </summary>
    /// <seealso cref="ICommand" />
    public abstract class SwitchableWpfCommandBase : ICommand
    {
        #region Fields

        private bool isEnabled = true;

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Properties

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled == value)
                    return;
                isEnabled = value;
                OnCanExecuteChanged(EventArgs.Empty);
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        public abstract void Execute(object parameter);

        #endregion

        #region Protected Methods

        protected virtual bool EvaluateCanExecute(object parameter) => IsEnabled;

        protected virtual void OnCanExecuteChanged(EventArgs e) => CanExecuteChanged?.Invoke(this, e);

        #endregion

        #region Explicitly Implemented Interface Methods

        bool ICommand.CanExecute(object parameter) => EvaluateCanExecute(parameter);

        #endregion

        #endregion
    }
}
