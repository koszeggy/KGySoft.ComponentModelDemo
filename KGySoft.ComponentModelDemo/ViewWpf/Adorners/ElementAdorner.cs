#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Adorners
{
    // By setting the ElementAdorner.Template attached property you can define a template for a control in the adorner layer.
    // I created this class in order to be able to create templates for the Warning and Information validation levels similarly
    // to the already existing Validation.ErrorTemplate property. See the MainWindow.xaml for examples and also the
    // ValidationResult/HasValidationResult markup extensions, which can be used for the validation results of IValidatingObject instances.
    /// <summary>
    /// Contains the <see cref="TemplateProperty"/> attached property that can be used to specify an adorner for any <see cref="UIElement"/> as a <see cref="ControlTemplate"/>.
    /// </summary>
    public static class ElementAdorner
    {
        #region Fields

        #region Public Fields

        public static readonly DependencyProperty TemplateProperty = DependencyProperty.RegisterAttached(
                "Template", typeof(ControlTemplate), typeof(ElementAdorner), new PropertyMetadata(OnTemplateChanged));

        #endregion

        #region Internal Fields

        internal static readonly DependencyProperty UnderlyingAdornerProperty = DependencyProperty.RegisterAttached(
                "UnderlyingAdorner", typeof(TemplatedAdorner), typeof(ElementAdorner));
        internal static readonly DependencyPropertyKey AdornedParentPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
                "AdornedParent", typeof(UIElement), typeof(ElementAdorner), new PropertyMetadata(null));
        internal static readonly DependencyProperty AdornedParentProperty = AdornedParentPropertyKey.DependencyProperty;

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        public static ControlTemplate GetTemplate(UIElement target) => (ControlTemplate)target.GetValue(TemplateProperty);
        public static void SetTemplate(UIElement target, ControlTemplate value) => target.SetValue(TemplateProperty, value);

        #endregion

        #region Internal Methods

        internal static TemplatedAdorner GetUnderlyingAdorner(UIElement target) => (TemplatedAdorner)target.GetValue(UnderlyingAdornerProperty);
        internal static void SetUnderlyingAdorner(UIElement target, TemplatedAdorner value) => target.SetValue(UnderlyingAdornerProperty, value);
        internal static void SetAdornedParent(DependencyObject target, UIElement adornedElement) => target.SetValue(AdornedParentPropertyKey, adornedElement);

        #endregion

        #region Private Methods

        private static void UpdateAdorner(UIElement adornedElement)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(adornedElement);
            if (layer == null)
            {
                // The same logic as in Validation.ShowValidationAdornerHelper:
                // There is no adorner layer maybe because the window is not created yet: postponing
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Loaded,
                    new Action<UIElement>(UpdateAdorner), adornedElement);
                return;
            }

            TemplatedAdorner existingAdorner = GetUnderlyingAdorner(adornedElement);
            if (existingAdorner != null)
                layer.Remove(existingAdorner);

            ControlTemplate controlTemplate = GetTemplate(adornedElement);
            if (controlTemplate == null)
            {
                SetUnderlyingAdorner(adornedElement, null);
                return;
            }

            var newAdorner = new TemplatedAdorner(adornedElement, controlTemplate);
            layer.Add(newAdorner);
            SetUnderlyingAdorner(adornedElement, newAdorner);
        }

        #endregion

        #region Event handlers

        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => UpdateAdorner((UIElement)d);

        #endregion

        #endregion
    }
}
