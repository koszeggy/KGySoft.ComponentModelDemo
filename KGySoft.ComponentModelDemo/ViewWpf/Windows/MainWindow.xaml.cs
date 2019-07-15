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
using KGySoft.ComponentModelDemo.ViewWpf.Commands;
using KGySoft.CoreLibraries;
using KGySoft.Reflection;
using WpfCommand = System.Windows.Input.ICommand;
using KGyCommand = KGySoft.ComponentModel.ICommand;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private readonly MainViewModel viewModel;

        private ICollectionView currentView;

        #endregion

        #region Properties

        // A shared state for the command bindings, which are enabled if there is a selected current item available.
        // See the XAML file where the Del/Item and Prop buttons reference this state. Please also note how KGy SOFT commands separate the static
        // execution logic (which is in the ViewModel now) from the dynamic state (which is here, in the View).
        // See also the EditToolBar class for another solution: there the KGy SOFT Commands are wrapped into WPF commands by the KGyCommandAdapter class.
        public ICommandState IsCurrentItemAvailable { get; } = new CommandState { Enabled = false };

        // This is a local command so for this we use a pure WPF command.
        public WpfCommand ResetBindingCommand { get; }

        #endregion

        #region Constructors

        public MainWindow() : this(null)
        {
        }

        public MainWindow(MainViewModel viewModel)
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

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
            IsCurrentItemAvailable.Enabled = ((ICollectionView)sender).CurrentItem is ITestObject;
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
