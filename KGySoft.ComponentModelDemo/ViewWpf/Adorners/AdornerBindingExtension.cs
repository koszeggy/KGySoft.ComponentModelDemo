using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace KGySoft.ComponentModelDemo.ViewWpf.Adorners
{
    public class AdornerBindingExtension : MarkupExtension
    {
        public string Path { get; set; }

        public AdornerBindingExtension()
        {
        }

        public AdornerBindingExtension(string path) => Path = path;

        public override object ProvideValue(IServiceProvider serviceProvider)
          => new Binding
          {
              RelativeSource = RelativeSource.TemplatedParent,
              Path = String.IsNullOrWhiteSpace(Path)
                  ? new PropertyPath(ElementAdorner.AdornedParentProperty)
                  : new PropertyPath($"(0).{Path}", ElementAdorner.AdornedParentProperty)
          };
    }
}
