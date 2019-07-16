#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Adorners
{
    // This is the actual underlying adorner created when <see cref="ElementAdorner.TemplateProperty"/> is set.
    // Similar to the original internal WPF class <c>TemplatedAdorner</c> used exclusively for validation error templates.
    internal sealed class TemplatedAdorner : Adorner
    {
        #region Fields

        private readonly Control adornerHost;

        #endregion

        #region Properties

        #region Internal Properties

        internal AdornedElementPlaceholder Placeholder { get; set; }

        #endregion

        #region Protected Properties

        protected override int VisualChildrenCount => adornerHost != null ? 1 : 0;

        #endregion

        #endregion

        #region Constructors

        internal TemplatedAdorner(UIElement adornedElement, ControlTemplate adornerTemplate)
            : base(adornedElement)
        {
            if (adornedElement == null)
                throw new ArgumentNullException(nameof(adornedElement));
            if (adornerTemplate == null)
                throw new ArgumentNullException(nameof(adornerTemplate));

            // Creating a new control for the adorner template so if the adornedElement has a template it will not overridden
            adornerHost = new Control
            {
                IsTabStop = false,
                Focusable = false,
                Template = adornerTemplate // this means that TemplatedParent will return this control instead of the adorned one
            };

            AddVisualChild(adornerHost);

            // NOTE: Iterating through the children after Loaded via VisualTreeHelper.GetChild is too late because bindings are evaluated before loading.
            // And we cannot get the elements of ControlTemplate either. So we just set the adorned parent for the host only.
            // (Originally I wanted to provide an AdornedParent to every element of the AdornerTemplate similarly to the TemplatedParent property.)
            ElementAdorner.SetAdornedParent(adornerHost, adornedElement);
        }

        #endregion

        #region Methods

        #region Public Methods

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            if (Placeholder == null)
                return transform;

            GeneralTransformGroup group = new GeneralTransformGroup();
            group.Children.Add(transform);

            GeneralTransform t = TransformToDescendant(Placeholder);
            if (t != null)
                group.Children.Add(t);
            return group;
        }

        #endregion

        #region Protected Methods

        protected override Visual GetVisualChild(int index)
        {
            if (adornerHost == null || index != 0)
                throw new ArgumentOutOfRangeException();
            return adornerHost;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (Placeholder != null && AdornedElement.IsMeasureValid && Placeholder.DesiredSize != AdornedElement.DesiredSize)
                Placeholder.InvalidateMeasure();
            adornerHost.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            return (adornerHost).DesiredSize;
        }

        protected override Size ArrangeOverride(Size size)
        {
            Size finalSize = base.ArrangeOverride(size);
            adornerHost?.Arrange(new Rect(new Point(), finalSize));
            return finalSize;
        }

        #endregion

        #endregion
    }
}
