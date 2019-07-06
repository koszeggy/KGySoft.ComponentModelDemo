using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.CoreLibraries;
using KGySoft.Reflection;

namespace KGySoft.ComponentModelDemo.ViewModel
{
    public class MainViewModel : ObservableObjectBase
    {
        private static readonly HashSet<string>[] radioGroups =
        {
            new HashSet<string> { nameof(UseList), nameof(UseBindingList), nameof(UseSortableBindingList), nameof(UseSortableBindingListSortOnChange), nameof(UseObservableCollection), nameof(UseObservableBindingList) },
            new HashSet<string> { nameof(NoInnerList), nameof(InnerList), nameof(InnerBindingList), nameof(InnerSortableBindingList), nameof(InnerObservableCollection), nameof(InnerObservableBindingList) },
            new HashSet<string> { nameof(UseObject), nameof(UseObservableObject), nameof(UseUndoableObject), nameof(UseEditableObject), nameof(UseValidatingObject), nameof(UseModelBase) }
        };

        // possible lists to bind
        public bool UseList { get => Get(true); set => Set(value); }
        public bool UseBindingList { get => Get(false); set => Set(value); }
        public bool UseSortableBindingList { get => Get(false); set => Set(value); }
        public bool UseSortableBindingListSortOnChange { get => Get(false); set => Set(value); }
        public bool UseObservableCollection { get => Get(false); set => Set(value); }
        public bool UseObservableBindingList { get => Get(false); set => Set(value); }

        // possible wrapped lists of the bound list
        public bool NoInnerList { get => Get(true); set => Set(value); }
        public bool InnerList { get => Get(false); set => Set(value); }
        public bool InnerBindingList { get => Get(false); set => Set(value); }
        public bool InnerSortableBindingList { get => Get(false); set => Set(value); }
        public bool InnerObservableCollection { get => Get(false); set => Set(value); }
        public bool InnerObservableBindingList { get => Get(false); set => Set(value); }

        // possible element base types
        public bool UseObject { get => Get(true); set => Set(value); }
        public bool UseObservableObject { get => Get(false); set => Set(value); }
        public bool UseUndoableObject { get => Get(false); set => Set(value); }
        public bool UseEditableObject { get => Get(false); set => Set(value); }
        public bool UseValidatingObject { get => Get(false); set => Set(value); }
        public bool UseModelBase { get => Get(false); set => Set(value); }

        public IList TestList { get => Get(GenerateList); set => Set(value); }
        public Random RandomInstance => Get(() => new Random());
        public bool ChangeUnderlyingCollection { get => Get(false); set => Set(value); }

        protected override void OnPropertyChanged(PropertyChangedExtendedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.NewValue is true && radioGroups.FirstOrDefault(g => g.Contains(e.PropertyName)) is IEnumerable<string> group)
            {
                AdjustRadioGroup(e.PropertyName, group);
                TestList = GenerateList();
            }
        }

        private void AdjustRadioGroup(string propertyName, IEnumerable<string> group)
        {
            foreach (string prop in group)
            {
                if (prop != propertyName)
                    Set(false, propertyName: prop);
            }
        }

        private IList GenerateList()
        {
            Type listType = UseList ? typeof(List<>)
                : UseBindingList ? typeof(FastBindingList<>)
                : UseSortableBindingList || UseSortableBindingListSortOnChange ? typeof(SortableBindingList<>)
                : UseObservableCollection ? typeof(ObservableCollection<>)
                : UseObservableBindingList ? typeof(ObservableBindingList<>)
                : throw new InvalidOperationException("No list type is selected");

            Type elementType = UseObject ? typeof(PocoTestObject)
                : UseObservableObject ? typeof(ObservableTestObject)
                : UseUndoableObject ? typeof(UndoableTestObject)
                : UseEditableObject ? typeof(EditableTestObject)
                : UseValidatingObject ? typeof(ValidatingTestObject)
                : UseModelBase ? typeof(FullExtraTestObject)
                : throw new InvalidOperationException("No element type is selected");

            Type innerListType = NoInnerList ? null
                : InnerList ? typeof(List<>)
                : InnerBindingList ? typeof(FastBindingList<>)
                : InnerSortableBindingList ? typeof(SortableBindingList<>)
                : InnerObservableCollection ? typeof(ObservableCollection<>)
                : InnerObservableBindingList ? typeof(ObservableBindingList<>)
                : throw new InvalidOperationException("No inner list type is selected");

            IList result = CreateList(listType, elementType, innerListType);
            if (UseSortableBindingListSortOnChange)
                Reflector.SetProperty(result, "SortOnChange", true);

            var rnd = RandomInstance;
            int length = rnd.NextInt32(2, 3, true);
            for (int i = 0; i < length; i++)
            {
                ITestObject item = UseObject ? rnd.NextObject<PocoTestObject>()
                    : UseObservableObject ? rnd.NextObject<ObservableTestObject>()
                    : UseUndoableObject ? rnd.NextObject<UndoableTestObject>()
                    : UseEditableObject ? rnd.NextObject<EditableTestObject>()
                    : UseValidatingObject ? rnd.NextObject<ValidatingTestObject>()
                    : (ITestObject)rnd.NextObject<FullExtraTestObject>();

                (item as ObservableObjectBase)?.SetModified(false);
                (item as ICanUndo)?.ClearUndoHistory();
                result.Add(item);
            }

            return result;
        }

        private IList CreateList(Type type, Type elementType, Type innerListType = null)
        {
            object[] parameters = innerListType == null ? null : new object[] { CreateList(innerListType, elementType, null) };
            Type genericType = type.MakeGenericType(elementType);
            return (IList)Reflector.CreateInstance(genericType, ReflectionWays.Auto, parameters);
        }
    }
}
