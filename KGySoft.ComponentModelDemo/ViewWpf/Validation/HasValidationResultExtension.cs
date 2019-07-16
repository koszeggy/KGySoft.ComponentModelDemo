using System;
using System.Globalization;
using KGySoft.ComponentModel;

namespace KGySoft.ComponentModelDemo.ViewWpf.Validation
{
    public class HasValidationResultExtension : ValidationResultExtension
    {
        public HasValidationResultExtension()
        {
        }

        public HasValidationResultExtension(ValidationSeverity severity) : base(severity)
        {
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => base.Convert(value, targetType, parameter, culture) != null;
    }
}