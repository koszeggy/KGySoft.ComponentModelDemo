using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KGySoft.ComponentModel;
using KGySoft.Reflection;

namespace BindingTest.ViewWinForms
{
    /// <summary>
    /// A specialized <see cref="CommandBindingsCollection"/> that can be used commands for <see cref="Control"/> sources.
    /// </summary>
    /// <seealso cref="KGySoft.ComponentModel.CommandBindingsCollection" />
    public class WinformsCommandBindingsCollection : CommandBindingsCollection
    {
        public override ICommandBinding Add(ICommand command, IDictionary<string, object> initialState = null, bool disposeCommand = false)
            => base.Add(command, initialState, disposeCommand)
                .AddStateUpdater(PropertyCommandStateUpdater.Updater)
                .AddStateUpdater(TooltipUpdater.Updater);
    }
}
