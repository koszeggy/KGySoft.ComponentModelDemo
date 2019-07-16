#region Usings

using System;
using System.Linq;

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.Model
{
    // A model class that implements IValidatingObject (KGySoft) and IDataErrorInfo (Microsoft) interfaces.
    // IDataErrorInfo is supported by several UI environments (WPF, WinForms, ASP.NET, etc.).
    // For IValidatingObject demo see the  ViewWpf.Validation.ValidationBindingExtension and
    // ViewWinForms.Components.ValidationResultToErrorProviderAdapter classes.
    public class ValidatingTestObject : ValidatingObjectBase, ITestObject
    {
        #region Properties

        public int IntProp { get => Get<int>(); set => Set(value); }
        public string StringProp { get => Get<string>(); set => Set(value); }

        #endregion

        #region Methods

        // This is how validation of the properties can be implemented. Error, Warning and Information severities can be used.
        // Changing a property value automatically causes the invalidation of earlier results.
        protected override ValidationResultsCollection DoValidation()
        {
            var result = new ValidationResultsCollection();
            if (IntProp == 0)
                result.AddError(nameof(IntProp), $"{nameof(IntProp)} must not be zero.");
            if (IntProp < 0)
                result.AddWarning(nameof(IntProp), $"{nameof(IntProp)} is recommended to be a non-negative value.");

            if (String.IsNullOrEmpty(StringProp))
                result.AddError(nameof(StringProp), $"{nameof(StringProp)} must not be null or empty.");
            if (StringProp?.All(Char.IsWhiteSpace) == true)
                result.AddWarning(nameof(StringProp), $"{nameof(StringProp)} contains only whitespace characters, which is not recommended.");
            if (StringProp?.Any(c => c < 32 || c > 127) == true)
                result.AddInfo(nameof(StringProp), $"{nameof(StringProp)} contains non-ASCII characters.");
            return result;
        }

        #endregion
    }
}
