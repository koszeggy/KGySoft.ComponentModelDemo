using System;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    public class ParameterizedWpfCommand<T> : WpfCommandBase
    {
        private readonly Action<T> callback;
        private readonly Func<T, bool> canExecute;

        public ParameterizedWpfCommand(Action<T> callback, Func<T, bool> canExecute = null)
        {
            this.callback = callback;
            this.canExecute = canExecute;
        }

        protected override bool DoCanExecute(object parameter)
        {
            if (canExecute != null)
                CanExecute = canExecute.Invoke((T)parameter);
            return CanExecute;
        }

        public override void Execute(object parameter) => callback.Invoke((T)parameter);
    }
}