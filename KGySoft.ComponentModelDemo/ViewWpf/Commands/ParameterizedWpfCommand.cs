using System;

namespace KGySoft.ComponentModelDemo.ViewWpf.Commands
{
    public class ParameterizedWpfCommand<T> : SwitchableWpfCommandBase
    {
        private readonly Action<T> callback;
        private readonly Func<T, bool> canExecute;

        public ParameterizedWpfCommand(Action<T> callback, Func<T, bool> canExecute = null)
        {
            this.callback = callback;
            this.canExecute = canExecute;
        }

        protected override bool EvaluateCanExecute(object parameter)
        {
            if (canExecute != null)
                IsEnabled = canExecute.Invoke((T)parameter);
            return IsEnabled;
        }

        public override void Execute(object parameter) => callback.Invoke((T)parameter);
    }
}