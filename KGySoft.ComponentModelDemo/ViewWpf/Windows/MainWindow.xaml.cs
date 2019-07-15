#region Used Namespaces

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.ComponentModelDemo.ViewModel;
using KGySoft.ComponentModelDemo.ViewWpf.Commands;
using KGySoft.CoreLibraries;

#endregion

#region Used Aliases

// See also the public properties and the constructor where there are examples for both kind of commands.
using MsCommand = System.Windows.Input.ICommand;
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

        // From C# code we always can create the bindings between events and KGy SOFT commands by an CommandBindingsCollection instance.
        // From XAML we can use the EventToKGyCommand extension and we can wrap KGy SOFT commands into WPF commands by the KGyCommandAdapter class.
        private readonly CommandBindingsCollection commandBindings = new CommandBindingsCollection();

        // This is a binding that we dynamically add and remove to/from the collection so we store a reference to it
        private ICommandBinding currentItemChangedBinding;

        private ICollectionView currentView;

        #endregion

        #region Properties

        // These public properties are accessed from the XAML file.

        // This is a local command for a button click and we use a pure "WPF command" (it is actually a System.dll type) for it.
        // See also the constructor. Other buttons in the XAML file reference KGy SOFT commands defined in the ViewModel. See also the XAML file.
        public MsCommand ResetBindingCommand { get; }

        // This is a command for the Window's Close event. But in XAML we cannot define a command for Window.Close so
        // we define a KGy SOFT command for it and in XAML we use the EventToKGyCommand extension.
        public KGyCommand WindowClosedCommand { get; }

        // Similarly, a KGy SOFT command for the DataGrid.AutoGeneratingColumn event. Note: We didn't need to define a public property if we created a
        // binding for the commands purely from C# instead of from XAML. See also the comments in the constructor.
        public KGyCommand AutoGeneratingColumnCommand { get; }

        // A shared state for the command bindings, which are enabled if there is a selected current item available.
        // See the XAML file where the Del/Item and Prop buttons reference this state. Please also note how KGy SOFT commands separate the static
        // execution logic (which is in the ViewModel now) from the dynamic state (which is here, in the View).
        // See also the EditToolBar class for another solution: there the KGy SOFT Commands are wrapped into WPF commands by the KGyCommandAdapter class.
        public ICommandState IsCurrentItemAvailable { get; } = new CommandState { Enabled = false };

        #endregion

        #region Constructors

        public MainWindow() : this(null)
        {
        }

        public MainWindow(MainViewModel viewModel)
        {
            // Initialization of a pure WPF command.
            // SimpleWpfCommand is actually a usual "relay" or "delegate" command.
            ResetBindingCommand = new SimpleWpfCommand(OnResetBindingCommand);

            // Initialization of a simple KGy SOFT command. The difference is only the access of these properties from XAML.
            // Instead of the XAML way we could have written: commandBindings.Add(new SimpleCommand(OnWindowClosedCommand)).AddSource(this, "Closed");
            WindowClosedCommand = new SimpleCommand(OnWindowClosedCommand);

            // Similarly, this could have been purely initialized from C# and then we didn't need the public property. See also the examples below.
            AutoGeneratingColumnCommand = new SourceAwareCommand<DataGridAutoGeneratingColumnEventArgs>(OnAutoGeneratingColumnCommand);

            InitializeComponent();
            DataContext = viewModel;
            this.viewModel = viewModel;

            // And of course we always can use a CommandBindingsCollection instance to create command bindings for any events.
            // This is highly recommended for events of objects that can outlive our window so we can release all of the handlers
            // at once by disposing the CommandBindingsCollection. See also the WindowClosedCommand.

            // Application.Current.DispatcherUnhandledException -> OnCurrentDispatcherUnhandledExceptionCommand
            // Note that we use an implicit command here. It is the shorthand of .Add(new SourceAwareCommand<DispatcherUnhandledExceptionEventArgs>(OnCurrentDispatcherUnhandledExceptionCommand))
            commandBindings.Add<DispatcherUnhandledExceptionEventArgs>(OnCurrentDispatcherUnhandledExceptionCommand)
                .AddSource(Application.Current, nameof(Application.DispatcherUnhandledException));

            // viewModel.PropertyChanged -> OnViewModelPropertyChangedCommand;
            commandBindings.Add<PropertyChangedEventArgs>(OnViewModelPropertyChangedCommand)
                .AddSource(viewModel, nameof(viewModel.PropertyChanged));

            UpdateCurrentView();
        }

        #endregion

        #region Methods

        #region Private Methods

        private void UpdateCurrentView()
        {
            // We remove the earlier binding if any, which releases it subscription automatically.
            if (currentItemChangedBinding != null)
                commandBindings.Remove(currentItemChangedBinding);

            currentView = CollectionViewSource.GetDefaultView(viewModel.TestList);

            // currentView.CurrentChanged -> OnTestListCurrentChangedCommand
            currentItemChangedBinding = commandBindings.Add<EventArgs>(OnTestListCurrentChangedCommand)
                .AddSource(currentView, nameof(currentView.CurrentChanged));
        }

        private void ResetBinding() => currentView.Refresh();

        #endregion

        #region View-related Command Handlers

        private void OnResetBindingCommand() => ResetBinding();

        private void OnWindowClosedCommand()
        {
            // Here we release all of the internally created event handlers maintained by this CommandBindingsCollection instance.
            // The event-command bindings defined in the XAML file are maintained by the WPF engine.
            commandBindings.Dispose();
        }

        private void OnCurrentDispatcherUnhandledExceptionCommand(ICommandSource<DispatcherUnhandledExceptionEventArgs> src)
        {
            ResetBinding();
            MessageBox.Show($"An unhandled exception has been detected, which would crash a regular application. The binding have been reset to prevent further errors.{Environment.NewLine}{Environment.NewLine}"
                + $"The caught exception: {src.EventArgs.Exception}", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            src.EventArgs.Handled = true;
        }

        private void OnViewModelPropertyChangedCommand(ICommandSource<PropertyChangedEventArgs> src)
        {
            if (src.EventArgs.PropertyName == nameof(viewModel.TestList))
                UpdateCurrentView();
        }

        private void OnTestListCurrentChangedCommand(ICommandSource<EventArgs> src)
        {
            // This is how we can change the enabled state of the commands that are associated with this state (CanExecute in the WPF world).
            // As this binding is created in XAML by the EventToKGyCommand extension the syncing of the IsEnabled properties is implemented there as well.
            // If this handler belonged to an ICommandBinding created by an CommandBindingsCollection we could use state updaters for syncing properties.
            // For an example for ICommandStateUpdaters see the WinformsCommandBindingsCollection class in the ViewWinForms namespace.
            IsCurrentItemAvailable.Enabled = ((ICollectionView)src.Source).CurrentItem is ITestObject;
        }

        private void OnAutoGeneratingColumnCommand(ICommandSource<DataGridAutoGeneratingColumnEventArgs> src)
        {
            if (src.EventArgs.PropertyName.In(nameof(ITestObject.IntProp), nameof(ITestObject.StringProp)))
            {
                var column = (DataGridBoundColumn)src.EventArgs.Column;
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
