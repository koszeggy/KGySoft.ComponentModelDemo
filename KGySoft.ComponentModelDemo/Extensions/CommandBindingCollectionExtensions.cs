using System;
using KGySoft.ComponentModel;

namespace KGySoft.ComponentModelDemo.Extensions
{
    internal static class CommandBindingsCollectionExtensions
    {
        public static void AddTwoWayPropertyBinding(this CommandBindingsCollection collection, object source, string sourcePropertyName, object target, string targetPropertyName = null, Func<object, object> format = null, Func<object, object> parse = null)
        {
            collection.AddPropertyBinding(source, sourcePropertyName, targetPropertyName ?? sourcePropertyName, format, target);
            collection.AddPropertyBinding(target, targetPropertyName ?? sourcePropertyName, sourcePropertyName, parse, source);
        }
    }
}
