using System.Windows;
using System.Windows.Data;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    public static class BindingHelper
    {
        private static readonly DependencyProperty dummyProperty = DependencyProperty.RegisterAttached("dummy", typeof(object), typeof(DependencyObject));

        public static object Evaluate(this BindingBase binding, DependencyObject target)
        {
            if (binding == null)
                return null;
            var obj = target ?? new DependencyObject();
            BindingOperations.SetBinding(obj, dummyProperty, binding);
            object result = obj.GetValue(dummyProperty);
            BindingOperations.ClearBinding(obj, dummyProperty);
            return result;
        }
    }
}