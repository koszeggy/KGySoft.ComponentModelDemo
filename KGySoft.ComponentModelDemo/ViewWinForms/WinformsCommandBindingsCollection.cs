using System.Collections.Generic;
using System.Windows.Forms;
using KGySoft.ComponentModel;

namespace KGySoft.ComponentModelDemo.ViewWinForms
{
    /// <summary>
    /// A specialized <see cref="CommandBindingsCollection"/> that can be used commands for <see cref="Control"/> sources.
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
