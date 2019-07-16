#region Usings

using System;

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo
{
    /// <summary>
    /// Extensions for the <see cref="CommandBindingsCollection"/> class.
    /// </summary>
    internal static class CommandBindingsCollectionExtensions
    {
        #region Methods

        public static void AddTwoWayPropertyBinding(this CommandBindingsCollection collection, object source, string sourcePropertyName, object target,
            string targetPropertyName = null, Func<object, object> format = null, Func<object, object> parse = null)
        {
            collection.AddPropertyBinding(source, sourcePropertyName, targetPropertyName ?? sourcePropertyName, format, target);
            collection.AddPropertyBinding(target, targetPropertyName ?? sourcePropertyName, sourcePropertyName, parse, source);
        }

        #endregion
    }
}
