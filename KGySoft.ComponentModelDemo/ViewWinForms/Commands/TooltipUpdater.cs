#region Usings

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using KGySoft.ComponentModel;
using KGySoft.Reflection;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWinForms.Commands
{
    // Demonstrates an ICommandStateUpdater implementation for ToolTips: It can apply the ToolTipText property of an ICommandState to the command source even if
    // the source control has no ToolTipText property but the parent form has a ToolTip component.
    // See also the WinformsCommandBindingsCollection class.
    public class TooltipUpdater : ICommandStateUpdater
    {
        #region Constants

        public const string ToolTipTextProperty = "ToolTipText";

        #endregion

        #region Fields

        private static readonly TooltipUpdater instance = new TooltipUpdater();

        #endregion

        #region Properties

        internal static ICommandStateUpdater Updater => instance;

        #endregion

        #region Constructors

        private TooltipUpdater() { }

        #endregion

        #region Methods

        #region Static Methods

        private static ToolTip GetToolTip(Control ctrl)
        {
            for (Control c = ctrl; c != null; c = c.Parent)
            {
                var containerFields = c.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(f => typeof(IContainer).IsAssignableFrom(f.FieldType));
                foreach (var containerField in containerFields)
                {
                    var container = (IContainer)Reflector.GetField(c, containerField);
                    var toolTip = container?.Components?.OfType<ToolTip>().FirstOrDefault();
                    if (toolTip != null)
                        return toolTip;
                }
            }

            return null;
        }

        #endregion

        #region Instance Methods

        #region Public Methods

        // This method has to return true if this updater instance could handle the specified state (ICommandBinding property).
        public bool TryUpdateState(object commandSource, string stateName, object value)
        {
            if (stateName != ToolTipTextProperty || !(value is string text) || !(commandSource is Control control))
                return false;

            ToolTip tooltip = GetToolTip(control);
            if (tooltip == null)
                return false;
            tooltip.SetToolTip(control, text);
            return true;
        }

        #endregion

        #region Explicitly Implemented Interface Methods

        void IDisposable.Dispose() { }

        #endregion

        #endregion

        #endregion
    }
}
