#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.ComponentModelDemo.ViewModel;
using KGySoft.CoreLibraries;
using KGySoft.Reflection;
using WpfCommand = System.Windows.Input.ICommand;
using KGyCommand = KGySoft.ComponentModel.ICommand;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private readonly MainViewModel viewModel;

        // A shared state for the command bindings with the current item parameter to manage their CanExecute status
        private readonly CommandState commandsWithCurrentItemState;

        private ICollectionView currentView;

        #endregion

        #region Properties

        // Unlike in Model and ViewModel, these are regular System.Windows.Input.ICommand commands, which are used traditionally in WPF.
        // They can wrap KGySoft.ComponentModel.ICommand instances though - see the constructor and the KGyCommandAdapter class.
        // NOTE: These properties would not be needed if the XAML used direct binding to the MainViewModel commands by the ToKGyCommand extension.
        // See the XAML file for examples or the EditToolBar control where the ToKGyCommand extension is used instead of wrapping.
        public WpfCommand AddItemCommand { get; }
        public WpfCommand RemoveItemCommand { get; }
        public WpfCommand SetItemCommand { get; }
        public WpfCommand SetItemPropertyCommand { get; }
        public WpfCommand ResetBindingCommand { get; }

        #endregion

        #region Constructors

        public MainWindow() : this(null)
        {
        }

        public MainWindow(MainViewModel viewModel)
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            commandsWithCurrentItemState = new CommandState { Enabled = false };

            // These commands are mapped to KGySoft.ComponentModel.ICommand commands defined in the ViewModel by the KGyCommandAdapter class.
            // See also the comment in the XAML file and the EditToolBar and ToKGyCommandExtension classes for examples without wrapping.
            AddItemCommand = new KGyCommandAdapter(viewModel.AddItemCommand);
            RemoveItemCommand = new KGyCommandAdapter(viewModel.RemoveItemCommand, commandsWithCurrentItemState);
            SetItemCommand = new KGyCommandAdapter(viewModel.SetItemCommand, commandsWithCurrentItemState);
            SetItemPropertyCommand = new KGyCommandAdapter(viewModel.SetItemPropertyCommand, commandsWithCurrentItemState);

            // This can be a pure WPF command as the executed method is in this class.
            ResetBindingCommand = new SimpleWpfCommand(OnResetBindingCommand);

            // TODO: Simple and parameterized commands (the ones, which don't need to get the triggering source and event arguments) can be mapped easily
            // to KGySoft.ComponentModel.ICommand (see the KGyCommandAdapter class).

            InitializeComponent();

            DataContext = viewModel;
            this.viewModel = viewModel;

            // TODO: events to commands
            UpdateCurrentView();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void UpdateCurrentView()
        {
            if (currentView != null)
                currentView.CurrentChanged -= CurrentView_CurrentChanged;

            currentView = CollectionViewSource.GetDefaultView(viewModel.TestList);
            currentView.CurrentChanged += CurrentView_CurrentChanged;
        }

        private void CurrentView_CurrentChanged(object sender, EventArgs e)
        {
            commandsWithCurrentItemState.Enabled = ((ICollectionView)sender).CurrentItem is ITestObject;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.TestList))
                UpdateCurrentView();
        }

        #endregion

        #region Methods

        #region Private Methods

        private void ResetBinding() => currentView.Refresh();

        #endregion

        #region Command Handlers

        private void OnResetBindingCommand() => ResetBinding();

        #endregion
        
        #region Event handlers

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ResetBinding();
            MessageBox.Show($"An unhandled exception has been detected, which would crash a regular application. The binding have been reset to prevent further errors.{Environment.NewLine}{Environment.NewLine}"
                    + $"The caught exception: {e.Exception}", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
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

        #endregion

        #endregion
    }
}
