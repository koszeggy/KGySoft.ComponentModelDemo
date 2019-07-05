using KGySoft.ComponentModel;

namespace BindingTest.ViewWpf
{
    public class KGyCommandAdapter : WpfCommandBase
    {
        private readonly ICommand wrappedCommand;
        private readonly ICommandState state;

        public KGyCommandAdapter(ICommand command, ICommandState state = null)
        {
            wrappedCommand = command;
            this.state = state;
        }

        public override void Execute(object parameter)
        {
            wrappedCommand.Execute(null, state, parameter);
        }
    }
}