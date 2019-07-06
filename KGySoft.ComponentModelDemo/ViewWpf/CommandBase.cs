using System;
using System.Windows.Input;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    public abstract class WpfCommandBase : ICommand
    {
        private bool canExecute = true;

        bool ICommand.CanExecute(object parameter) => DoCanExecute(parameter);

        protected virtual bool DoCanExecute(object parameter) => CanExecute;

        public abstract void Execute(object parameter);

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

        protected virtual void OnCanExecuteChanged(EventArgs e) => CanExecuteChanged?.Invoke(this, e);

        public event EventHandler CanExecuteChanged;
    }
}