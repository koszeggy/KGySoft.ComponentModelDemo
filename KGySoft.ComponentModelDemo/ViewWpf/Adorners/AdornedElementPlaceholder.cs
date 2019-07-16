#region Usings

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Adorners
{
    // See also the MainWindow.xaml for examples and the ElementAdorner class for more description.
    /// <summary>
    /// Similar to <see cref="System.Windows.Controls.AdornedElementPlaceholder"/>, which can be used for error templates only.
    /// This placeholder can be used in any template for any <see cref="UIElement"/> applied by the <see cref="ElementAdorner.TemplateProperty"/>.
    /// </summary>
    [ContentProperty(nameof(Child))]
    public class AdornedElementPlaceholder : FrameworkElement, IAddChild
    {
        #region Fields

        private UIElement child;
        private TemplatedAdorner templatedAdorner;

        #endregion

        #region Properties

        #region Public Properties

        /// <summary>
        /// Gets the element for which the <see cref="AdornedElementPlaceholder"/> is reserving space.
        /// </summary>
        public UIElement AdornedElement => TemplatedAdorner?.AdornedElement;

        /// <summary>
        /// The single child of an <see cref="AdornedElementPlaceholder" />
        /// </summary>
        [DefaultValue(null)]
        public virtual UIElement Child
        {
            get => child;
            set
            {
                UIElement old = child;

                // ReSharper disable once PossibleUnintendedReferenceComparison
                if (old == value)
                    return;

                RemoveVisualChild(old);
                //need to remove old element from logical tree
                RemoveLogicalChild(old);
                child = value;

                AddVisualChild(child);
                AddLogicalChild(value);

                InvalidateMeasure();
            }
        }

        #endregion

        #region Protected Properties

        protected override int VisualChildrenCount => child == null ? 0 : 1;

        #endregion

        #region Private Properties

        private TemplatedAdorner TemplatedAdorner
        {
            get
            {
                if (templatedAdorner == null)
                {
                    // find the TemplatedAdorner
                    if (TemplatedParent is FrameworkElement templateParent)
                    {
                        templatedAdorner = VisualTreeHelper.GetParent(templateParent) as TemplatedAdorner;

                        if (templatedAdorner != null && templatedAdorner.Placeholder == null)
                        {
                            templatedAdorner.Placeholder = this;
                        }
                    }
                }

                return templatedAdorner;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Protected Methods

        protected override Visual GetVisualChild(int index)
        {
            if (child == null || index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            return child;
        }

        protected override void OnInitialized(EventArgs e)
        {
            if (TemplatedParent == null)
                throw new InvalidOperationException($"The {nameof(AdornedElementPlaceholder)} can be used only in a template");

            base.OnInitialized(e);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (TemplatedParent == null)
                throw new InvalidOperationException($"The {nameof(AdornedElementPlaceholder)} can be used only in a template");

            if (AdornedElement == null)
                return new Size();

            Size desiredSize = AdornedElement.RenderSize;
            Child?.Measure(desiredSize);
            return desiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Child?.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        #endregion

        #region Explicitly Implemented Interface Methods

        void IAddChild.AddChild(object value)
        {
            if (value == null)
                return;

            if (!(value is UIElement element))
                throw new ArgumentException($"{nameof(UIElement)} expected", nameof(value));

            if (Child != null)
                throw new ArgumentException("Only 1 child is allowed");

            Child = element;
        }

        void IAddChild.AddText(string text)
        {
            // we do not allow text except if only whitespace
            if (!String.IsNullOrWhiteSpace(text))
                throw new NotSupportedException("No text element is supported");
        }

        #endregion

        #endregion
    }
}
