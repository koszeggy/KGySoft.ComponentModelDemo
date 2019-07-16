using System.Collections.Generic;
using System.Windows.Forms;
using KGySoft.ComponentModel;

namespace KGySoft.ComponentModelDemo.ViewWinForms.Commands
{
    /// <summary>
    /// A specialized <see cref="CommandBindingsCollection"/> that can be used commands for <see cref="Control"/> sources.
    /// The <see cref="ICommandState"/> properties (eg. <see cref="ICommandState.Enabled"/> but also any other added property) of the added bindings will be synced with the command sources.
    /// </summary>
    /// <seealso cref="CommandBindingsCollection" />
    public class WinformsCommandBindingsCollection : CommandBindingsCollection
    {
        public override ICommandBinding Add(ICommand command, IDictionary<string, object> initialState = null, bool disposeCommand = false)
            => base.Add(command, initialState, disposeCommand)
                .AddStateUpdater(PropertyCommandStateUpdater.Updater)
                .AddStateUpdater(TooltipUpdater.Updater);
    }
}
