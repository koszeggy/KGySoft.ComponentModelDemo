#region Usings

using System;

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.Model
{
    // A model class that unifies the features of EditableTestObject, UndoableTestObject and ValidatingTestObject classes.
    public class AllInOneTestObject : ModelBase, ITestObject
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

        #endregion
    }
}
