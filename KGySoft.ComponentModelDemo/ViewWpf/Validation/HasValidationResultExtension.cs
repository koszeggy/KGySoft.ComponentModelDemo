using System;
using System.Globalization;
using KGySoft.ComponentModel;

namespace KGySoft.ComponentModelDemo.ViewWpf.Validation
{
    // Use this markup extension to get whether there is an Error, Warning or Information validation message for an IValidatingObject.
    // See the MainWindow.xaml for examples and more description.
    /// <summary>
    /// A markup extension and a converter to get whether there is a validation message for the specified severity from a <see cref="KGySoft.ComponentModel.ValidationResult"/>
    /// for an <see cref="IValidatingObject"/> instance.
    /// </summary>
    public class HasValidationResultExtension : ValidationResultExtension
    {
        public HasValidationResultExtension() { }
        public HasValidationResultExtension(ValidationSeverity severity) : base(severity) { }

        // We just returning whether the ValidationResult converter returned any message. Simple, eh?
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => base.Convert(value, targetType, parameter, culture) != null;
    }
}