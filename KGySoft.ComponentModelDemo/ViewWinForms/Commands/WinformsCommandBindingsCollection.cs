#region Usings

using System.Collections.Generic;
using System.Windows.Forms;

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWinForms.Commands
{
    /// <summary>
    /// A specialized <see cref="CommandBindingsCollection"/> that can be used for commands with <see cref="Control"/> sources.
    /// By using this collection the <see cref="ICommandState"/> properties (eg. <see cref="ICommandState.Enabled"/> but also any other added property)
    /// of the added bindings will be synced with the command sources.
    /// </summary>
    /// <seealso cref="CommandBindingsCollection" />
    public class WinformsCommandBindingsCollection : CommandBindingsCollection
    {
        #region Methods

        public override ICommandBinding Add(ICommand command, IDictionary<string, object> initialState = null, bool disposeCommand = false)
            => base.Add(command, initialState, disposeCommand)
                .AddStateUpdater(PropertyCommandStateUpdater.Updater) // for regular properties
                .AddStateUpdater(TooltipUpdater.Updater); // for tool tips via a System.Windows.Forms.ToolTip component.

        #endregion
    }
}
