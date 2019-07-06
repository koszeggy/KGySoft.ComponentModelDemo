using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.ComponentModelDemo.ViewModel;
using KGySoft.CoreLibraries;
using KGySoft.Reflection;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BindingTestWindow : Window
    {
        private readonly BindingViewModel viewModel;

        public ICommand ResetBindingCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ReplaceItemCommand { get; }
        public ICommand EditItemCommand { get; }

        public BindingTestWindow()
        {
            ResetBindingCommand = new SimpleWpfCommand(OnResetBindingCommand);
            AddItemCommand = new SimpleWpfCommand(OnAddItemCommand);
            RemoveItemCommand = new ParameterizedWpfCommand<ITestObject>(OnRemoveItemCommand);
            ReplaceItemCommand = new ParameterizedWpfCommand<ITestObject>(OnReplaceItemCommand);
            EditItemCommand = new ParameterizedWpfCommand<ITestObject>(OnEditItemCommand);

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            InitializeComponent();
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            OnResetBindingCommand();
            MessageBox.Show($"An unhandled exception has been detected, which would crash a regular application. The binding have been reset prevent further errors.{Environment.NewLine}{Environment.NewLine}"
                + $"The caught exception message: {e.Exception.Message}", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        public BindingTestWindow(BindingViewModel viewModel) : this()
        {
            DataContext = viewModel;
            this.viewModel = viewModel;
        }

        private void OnResetBindingCommand()
        {
            object context = DataContext;
            DataContext = null;
            DataContext = context;
        }

        private void OnAddItemCommand()
        {
            IList list = GetListToModify(viewModel.TestList);
            Type elementType = list.GetType().GetInterface(typeof(IList<>).Name).GetGenericArguments()[0];
            list.Add((ITestObject)Reflector.CreateInstance(elementType));
        }

        private void OnRemoveItemCommand(ITestObject item)
        {
            IList list = GetListToModify(viewModel.TestList);
            list.Remove(item);
        }

        private void OnReplaceItemCommand(ITestObject item)
        {
            IList list = GetListToModify(viewModel.TestList);
            int index = list.IndexOf(item);
            if (index < 0)
                return;
            list[index] = (ITestObject)viewModel.RandomInstance.NextObject(item.GetType());
        }

        private void OnEditItemCommand(ITestObject item)
        {
            item.StringProp = viewModel.RandomInstance.NextString();
        }

        private IList GetListToModify(IList list)
        {
            if (!viewModel.ChangeUnderlyingCollection)
                return list;

            PropertyInfo itemsProp = list.GetType().GetProperty("Items", BindingFlags.Instance | BindingFlags.NonPublic);
            return itemsProp == null ? list : (IList)itemsProp.GetValue(list, null);
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.In(nameof(ITestObject.IntProp), nameof(ITestObject.StringProp)))
            {
                var column = (DataGridBoundColumn)e.Column;
                var binding = (Binding)column.Binding;
                binding.ValidatesOnDataErrors = true;
                column.ElementStyle = (Style)FindResource("styleCellError");
                column.EditingElementStyle = (Style)FindResource("styleEditCellError");
            }
        }

        public string Random => ThreadSafeRandom.Instance.NextString();
    }
}
