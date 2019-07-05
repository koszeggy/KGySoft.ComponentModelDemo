using System;

namespace BindingTest.ViewWpf
{
    public class SimpleWpfCommand : WpfCommandBase
    {
        private readonly Action callback;
        private readonly Func<bool> canExecute;

        public SimpleWpfCommand(Action callback, Func<bool> canExecute = null)
        {
            this.callback = callback;
            this.canExecute = canExecute;
        }

        protected override bool DoCanExecute(object _)
        {
            if (canExecute != null)
                CanExecute = canExecute.Invoke();
            return CanExecute;
        }

        public override void Execute(object _) => callback.Invoke();
    }
}