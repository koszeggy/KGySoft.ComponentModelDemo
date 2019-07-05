using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BindingTest.ViewWpf.Adorners
{
    /// <summary>
    /// This is the actual underlying adorner created when <see cref="ElementAdorner.TemplateProperty"/> is set.
    /// Similar to the original internal WPF class <c>TemplatedAdorner</c> used exclusively for validation error templates.
    /// </summary>
    internal sealed class TemplatedAdorner : Adorner
    {
        private readonly UIElement adornedElement;
        private readonly Control adornerHost;
        internal AdornedElementPlaceholder Placeholder { get; set; }

        internal TemplatedAdorner(UIElement adornedElement, ControlTemplate adornerTemplate)
            : base(adornedElement)
        {
            if (adornedElement == null)
                throw new ArgumentNullException(nameof(adornedElement));
            if (adornerTemplate == null)
                throw new ArgumentNullException(nameof(adornerTemplate));

            this.adornedElement = adornedElement;

            // Creating a new control for the adorner template so if the adornedElement has a template it will not overridden
            adornerHost = new Control
            {
                IsTabStop = false, 
                Focusable = false,
                Template = adornerTemplate // this means that TemplatedParent will return this control instead of the adorned one
            };

            AddVisualChild(adornerHost);

            // NOTE: Iterating through the children after Loaded via VisualTreeHelper.GetChild is too late because bindings are evaluated before loading
            // And we cannot get the elements of ControltTemplate either. So we just set the adorned parent for the host only.
            ElementAdorner.SetAdornedParent(adornerHost, adornedElement);
        }


        ///// <summary>
        ///// Sets the adorned element recursively for the element so it can be obtained by bindings
        ///// NOTE: Iterating through the children via VisualTreeHelper.GetChild is too late because bindings are evaluated before loading
        ///// </summary>
        //private void SetAdornedElement(FrameworkElement element)
        //{
        //    if (element == null)
        //        return;
        //    ElementAdorner.SetAdornedParent(element, adornedElement);
        //    element.Unloaded += AdornerElement_Unloaded;
        //    switch (element)
        //    {
        //        case ContentControl contentControl when contentControl.Content is FrameworkElement childElement:
        //            SetAdornedElement(childElement);
        //            break;
        //        case ItemsControl itemsControl:
        //            foreach (object item in itemsControl.Items)
        //            {
        //                if (item is FrameworkElement childElement)
        //                    SetAdornedElement(childElement);
        //            }
        //            break;
        //        case Panel panel:
        //            foreach (object child in panel.Children)
        //            {
        //                if (child is FrameworkElement childElement)
        //                    SetAdornedElement(childElement);
        //            }
        //            break;
        //    }
        //}

        //private void AdornerElement_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    var element = (FrameworkElement)sender;
        //    ElementAdorner.SetAdornedParent(element, null);
        //    element.Unloaded -= AdornerElement_Unloaded;
        //}

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            if (Placeholder == null)
                return transform;

            GeneralTransformGroup group = new GeneralTransformGroup();
            group.Children.Add(transform);

            GeneralTransform t = this.TransformToDescendant(Placeholder);
            if (t != null)
            {
                group.Children.Add(t);
            }
            return group;
        }

        protected override int VisualChildrenCount => adornerHost != null ? 1 : 0;

        protected override Visual GetVisualChild(int index)
        {
            if (adornerHost == null || index != 0)
                throw new ArgumentOutOfRangeException();
            return adornerHost;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (Placeholder != null &&
                AdornedElement.IsMeasureValid &&
                Placeholder.DesiredSize != AdornedElement.DesiredSize)
            {
                Placeholder.InvalidateMeasure();
            }

            adornerHost.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            return (adornerHost).DesiredSize;
        }

        protected override Size ArrangeOverride(Size size)
        {
            Size finalSize = base.ArrangeOverride(size);
            adornerHost?.Arrange(new Rect(new Point(), finalSize));
            return finalSize;
        }
    }
}