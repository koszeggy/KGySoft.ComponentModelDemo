#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.CoreLibraries;
using KGySoft.Reflection;

#endregion

namespace KGySoft.ComponentModelDemo.ViewModel
{
    public class MainViewModel : ObservableObjectBase
    {
        #region Fields

        // The bool properties of these groups work as radio buttons even if they are set without any UI. See also the AdjustRadioGroup method.
        private static readonly HashSet<string>[] radioGroups =
            {
                new HashSet<string> { nameof(UseList), nameof(UseBindingList), nameof(UseSortableBindingList), nameof(UseSortableBindingListSortOnChange), nameof(UseObservableCollection), nameof(UseObservableBindingList) },
                new HashSet<string> { nameof(NoInnerList), nameof(InnerList), nameof(InnerBindingList), nameof(InnerSortableBindingList), nameof(InnerObservableCollection), nameof(InnerObservableBindingList) },
                new HashSet<string> { nameof(UseObject), nameof(UseObservableObject), nameof(UseUndoableObject), nameof(UseEditableObject), nameof(UseValidatingObject), nameof(UseModelBase) }
            };

        #endregion

        #region Events

        // note that even the handler is stored in the base instead of a backing field
        public event EventHandler<CommandErrorEventArgs> CommandError
        {
            add => CommandErrorHandler += value;
            remove => CommandErrorHandler -= value;
        }

        #endregion

        #region Properties

        #region Public Properties

        // The possible lists to bind. The first element of the "radio group" is on by default.
        // The auto toggling of the options is implemented in the overridden OnPropertyChanged method.
        public bool UseList { get => Get(true); set => Set(value); }
        public bool UseBindingList { get => Get(false); set => Set(value); }
        public bool UseSortableBindingList { get => Get(false); set => Set(value); }
        public bool UseSortableBindingListSortOnChange { get => Get(false); set => Set(value); }
        public bool UseObservableCollection { get => Get(false); set => Set(value); }
        public bool UseObservableBindingList { get => Get(false); set => Set(value); }

        // The possible wrapped lists of the bound list.
        public bool NoInnerList { get => Get(true); set => Set(value); }
        public bool InnerList { get => Get(false); set => Set(value); }
        public bool InnerBindingList { get => Get(false); set => Set(value); }
        public bool InnerSortableBindingList { get => Get(false); set => Set(value); }
        public bool InnerObservableCollection { get => Get(false); set => Set(value); }
        public bool InnerObservableBindingList { get => Get(false); set => Set(value); }

        // The possible element base types.
        public bool UseObject { get => Get(true); set => Set(value); }
        public bool UseObservableObject { get => Get(false); set => Set(value); }
        public bool UseUndoableObject { get => Get(false); set => Set(value); }
        public bool UseEditableObject { get => Get(false); set => Set(value); }
        public bool UseValidatingObject { get => Get(false); set => Set(value); }
        public bool UseModelBase { get => Get(false); set => Set(value); }

        // This will be the test list for binding in the different UI frameworks. The actual type depends on the values above.
        // The initial value is quite complex to evaluate so initialized by a delegate.
        public IList TestList { get => Get(GenerateList); set => Set(value); }

        // Determines whether the commands below manipulate the TestList or its wrapped list.
        // Since the default value of bool is false, the getter could have been written as Get(false) just like in the groups above.
        public bool ChangeUnderlyingCollection { get => Get<bool>(); set => Set(value); }

        // A read-only property with a one time initializer delegate.
        public Random RandomInstance => Get(() => new Random());

        // Unlike Model.Commands, these are instance commands, which use the MainModelView members.
        public ICommand AddItemCommand => Get(() => new SimpleCommand(OnAddItemCommand));
        public ICommand RemoveItemCommand => Get(() => new TargetedCommand<ITestObject>(OnRemoveItemCommand));
        public ICommand SetItemCommand => Get(() => new TargetedCommand<ITestObject>(OnSetItemCommand));
        public ICommand SetItemPropertyCommand => Get(() => new TargetedCommand<ITestObject>(OnSetItemPropertyCommand));

        #endregion

        #region Private Properties

        // We store even the event handler in the base storage. As this is a private property, we can disable invoking PropertyChange in the setter.
        private EventHandler<CommandErrorEventArgs> CommandErrorHandler { get => Get<EventHandler<CommandErrorEventArgs>>(); set => Set(value, false); }

        #endregion

        #endregion

        #region Methods

        #region Protected Methods

        protected override void OnPropertyChanged(PropertyChangedExtendedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.NewValue is true && radioGroups.FirstOrDefault(g => g.Contains(e.PropertyName)) is IEnumerable<string> group)
            {
                AdjustRadioGroup(e.PropertyName, group);
                TestList = GenerateList();
            }
        }

        #endregion

        #region Private Methods

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
                : UseBindingList ? typeof(BindingList<>)
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
                : InnerBindingList ? typeof(BindingList<>)
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

        private void OnCommandError(Exception e, [CallerMemberName]string operation = null)
        {
            var args = new CommandErrorEventArgs(e, operation);
            CommandErrorHandler?.Invoke(this, args);
            if (!args.Handled)
                throw new InvalidOperationException($"Operation '{operation}' failed. See inner exception for details.", e);
        }

        private IList GetListToAccess() => ChangeUnderlyingCollection && !UseList ? (IList)Reflector.GetProperty(TestList, "Items") : TestList;

        #endregion

        #region ViewModel-related Command Handlers

        private void OnAddItemCommand()
        {
            IList list = GetListToAccess();
            Type elementType = list.GetType().GetInterface(typeof(IList<>).Name).GetGenericArguments()[0];
            try
            {
                list.Add(Reflector.CreateInstance(elementType));
            }
            catch (Exception e)
            {
                OnCommandError(e);
            }
        }

        private void OnRemoveItemCommand(ITestObject item)
        {
            try
            {
                GetListToAccess().Remove(item);
            }
            catch (Exception e)
            {
                OnCommandError(e);
            }
        }

        private void OnSetItemCommand(ITestObject toBeReplaced)
        {
            IList list = GetListToAccess();
            Type elementType = list.GetType().GetInterface(typeof(IList<>).Name).GetGenericArguments()[0];
            try
            {
                list[list.IndexOf(toBeReplaced)] = RandomInstance.NextObject(elementType);
            }
            catch (Exception e)
            {
                OnCommandError(e);
            }
        }

        private void OnSetItemPropertyCommand(ITestObject item)
        {
            try
            {
                item.StringProp = RandomInstance.NextString();
            }
            catch (Exception e)
            {
                OnCommandError(e);
            }
        }

        #endregion

        #endregion
    }
}
