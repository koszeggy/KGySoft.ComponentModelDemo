using System;
using System.Linq;
using KGySoft.ComponentModel;

namespace KGySoft.ComponentModelDemo.Model
{
    public class ValidatingTestObject : ValidatingObjectBase, ITestObject
    {
        public int IntProp
        {
            get => Get<int>();
            set => Set(value);
        }

        public string StringProp
        {
            get => Get<string>();
            set => Set(value);
        }

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
    }
}