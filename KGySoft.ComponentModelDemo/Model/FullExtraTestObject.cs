using System;
using System.Linq;
using KGySoft.ComponentModel;

namespace BindingTest.Model
{
    public class FullExtraTestObject : ModelBase, ITestObject
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
            ValidationResultsCollection result = base.DoValidation();

            if (IntProp < 0)
                result.AddError(nameof(IntProp), $"{nameof(IntProp)} must be greater or equal to 0.");
            if (IntProp == 0)
                result.AddInfo(nameof(IntProp), $"{nameof(IntProp)} is 0, which is the default value.");
            if (IntProp > Int16.MaxValue)
                result.AddWarning(nameof(IntProp), $"{nameof(IntProp)} is larger than {Int16.MaxValue}. It is recommended to use a smaller value.");

            if (String.IsNullOrEmpty(StringProp))
                result.AddError(nameof(StringProp), $"{nameof(StringProp)} must not be null or empty.");
            if (StringProp?.Length < 3)
                result.AddWarning(nameof(StringProp), $"{nameof(StringProp)} is shorter than 3 characters.");
            return result;
        }
    }
}
