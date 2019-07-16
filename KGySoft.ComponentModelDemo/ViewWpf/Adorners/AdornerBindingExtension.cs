#region Usings

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Adorners
{
    // In XAML {adorners:AdornerBinding} is actually the shorthand of {Binding RelativeSource={RelativeSource TemplatedParent}, Path=(adorners:ElementAdorner.AdornedParent)}
    // See also the MainWindow.xaml for examples and the ElementAdorner class for more description.
    /// <summary>
    /// A markup extension for resolving the adorned parent control from an adorner template.
    /// </summary>
    public class AdornerBindingExtension : MarkupExtension
    {
        #region Properties

        public string Path { get; set; }

        #endregion

        #region Constructors

        public AdornerBindingExtension() { }
        public AdornerBindingExtension(string path) => Path = path;

        #endregion

        #region Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
            => new Binding
            {
                RelativeSource = RelativeSource.TemplatedParent,
                Path = String.IsNullOrWhiteSpace(Path)
                    ? new PropertyPath(ElementAdorner.AdornedParentProperty)
                    : new PropertyPath($"(0).{Path}", ElementAdorner.AdornedParentProperty)
            };

        #endregion
    }
}
