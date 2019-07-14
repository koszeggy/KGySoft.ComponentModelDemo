using System;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    /// <summary>
    /// A simple "relay" command from a delegate with a switchable <see cref="SwitchableWpfCommandBase.IsEnabled"/> property.
    /// The term "Wpf" in the name just denotes that it is an <see cref="System.Windows.Input.ICommand"/> and not a <see cref="KGySoft.ComponentModel.ICommand"/>.
    /// </summary>
    /// <seealso cref="SwitchableWpfCommandBase" />
    public class SimpleWpfCommand : SwitchableWpfCommandBase
    {
        private readonly Action callback;
        private readonly Func<bool> canExecute;

        public SimpleWpfCommand(Action callback, Func<bool> canExecute = null)
        {
            this.callback = callback;
            this.canExecute = canExecute;
        }

        protected override bool EvaluateCanExecute(object _)
        {
            if (canExecute != null)
                IsEnabled = canExecute.Invoke();
            return IsEnabled;
        }

        public override void Execute(object _) => callback.Invoke();
    }
}