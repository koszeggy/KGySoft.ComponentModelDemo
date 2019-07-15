using System.ComponentModel;
using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.ViewWpf.Commands;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    public class KGyCommandAdapter : SwitchableWpfCommandBase
    {
        private readonly ICommand wrappedCommand;
        private readonly ICommandState state;

        public KGyCommandAdapter(ICommand command, CommandState state = null)
        {
            wrappedCommand = command;
            if (state == null)
                return;

            this.state = state;
            state.CreatePropertyBinding(nameof(state.Enabled), nameof(IsEnabled), this);
        }

        public override void Execute(object parameter)
        {
            wrappedCommand.Execute(null, state, parameter);
        }
    }
}