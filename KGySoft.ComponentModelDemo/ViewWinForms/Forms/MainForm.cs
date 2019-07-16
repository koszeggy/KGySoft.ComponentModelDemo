#region Usings

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Extensions;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.ComponentModelDemo.ViewModel;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWinForms.Forms
{
    public partial class MainForm : Form
    {
        #region Fields

        private readonly MainViewModel viewModel;

        private readonly CommandBindingsCollection commandBindings = new CommandBindingsCollection();

        // A shared state for the command bindings with the current item parameter to manage their Enabled status
        private readonly ICommandState commandsWithCurrentItemState;

        // These bindings are dynamically removed and re-created so they are stored as fields
        private Binding intPropColorBinding, stringPropColorBinding;
        private ICommandBinding formatColorBinding;

        #endregion

        #region Constructors

        public MainForm(MainViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;

            errorProvider.Icon = Images.Error;
            warningProvider.Icon = Images.Warning;
            infoProvider.Icon = Images.Information;

            // Setting the TextBoxes of current elements, too; though they have a different source so no validation will appear for them
            // without manual setting or another set of providers bound to the itemBindingSource
            SetProviderPaddings(-20, tbIntPropList, tbStringPropList, tbIntPropCurrent, tbStringPropCurrent);

            // For the better overview even the standard WinForms bindings are set here instead of the designer.

            // listBindingSource -> grid/listBox/errorProvider/tbIntPropList/tbStringPropList/editMenuStrip
            tbIntPropList.DataBindings.Add(nameof(TextBox.Text), listBindingSource, nameof(ITestObject.IntProp));
            tbStringPropList.DataBindings.Add(nameof(TextBox.Text), listBindingSource, nameof(ITestObject.StringProp));
            editMenuStrip.DataBindings.Add(nameof(editMenuStrip.DataSource), listBindingSource, String.Empty);

            // itemBindingSource -> tbIntPropCurrent/tbStringPropCurrent
            tbIntPropCurrent.DataBindings.Add(nameof(TextBox.Text), itemBindingSource, nameof(ITestObject.IntProp));
            tbStringPropCurrent.DataBindings.Add(nameof(TextBox.Text), itemBindingSource, nameof(ITestObject.StringProp));

            // A ToolStripButton does not support regular WinForms binding. But as it has a CheckedChanged event, KGy SOFT's command binding can be used for it.
            // btnChangeInner.Checked -> viewModel.ChangeUnderlyingCollection - AddPropertyBinding will use an internal command for the change event.
            commandBindings.AddPropertyBinding(btnChangeInner, nameof(btnChangeInner.Checked), nameof(viewModel.ChangeUnderlyingCollection), viewModel);

            // Binding radio buttons by KGy SOFT's command binding, too (regular WinForms binding behaves strangely for radio buttons).
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseList), rbList, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseBindingList), rbBindingList, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseSortableBindingList), rbSortableBindingList, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseSortableBindingListSortOnChange), rbSortableBindingListSortOnChange, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseObservableCollection), rbObservableCollection, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseObservableBindingList), rbObservableBindingList, nameof(RadioButton.Checked));

            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.NoInnerList), rbNoInnerList, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.InnerList), rbInnerList, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.InnerBindingList), rbInnerBindingList, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.InnerSortableBindingList), rbInnerSortableBindingList, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.InnerObservableCollection), rbInnerObservableCollection, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.InnerObservableBindingList), rbInnerObservableBindingList, nameof(RadioButton.Checked));

            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseObject), rbObject, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseObservableObject), rbObservableObject, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseUndoableObject), rbUndoable, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseEditableObject), rbEditable, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseValidatingObject), rbValidating, nameof(RadioButton.Checked));
            commandBindings.AddTwoWayPropertyBinding(viewModel, nameof(viewModel.UseModelBase), rbModel, nameof(RadioButton.Checked));

            // Binding to ViewModel Commands. Using a shared state for the indexing commands so we can set their Enabled status at once.
            // Adding PropertyCommandStateUpdater.Updater to them so their source buttons will reflect the Enabled state.
            commandsWithCurrentItemState = new CommandState { Enabled = false };
            commandBindings.Add(viewModel.AddItemCommand, btnAdd, nameof(btnAdd.Click)); // btnAdd.Click -> viewModel.AddItemCommand
            commandBindings.Add(viewModel.RemoveItemCommand, commandsWithCurrentItemState) // btnRemove.Click -> viewModel.RemoveItemCommand
                .AddStateUpdater(PropertyCommandStateUpdater.Updater)
                .AddSource(btnRemove, nameof(btnRemove.Click))
                .AddTarget(() => listBindingSource.Current);
            commandBindings.Add(viewModel.SetItemCommand, commandsWithCurrentItemState) // btnSetItem.Click -> viewModel.SetItemCommand
                .AddStateUpdater(PropertyCommandStateUpdater.Updater)
                .AddSource(btnSetItem, nameof(btnSetItem.Click))
                .AddTarget(() => listBindingSource.Current);
            commandBindings.Add(viewModel.SetItemPropertyCommand, commandsWithCurrentItemState) // btnSetProp.Click -> viewModel.SetItemPropertyCommand
                .AddStateUpdater(PropertyCommandStateUpdater.Updater)
                .AddSource(btnSetProp, nameof(btnSetProp.Click))
                .AddTarget(() => listBindingSource.Current);

            // Note that the following bindings don't reference any explicitly defined ICommands instances. We can do this for private commands not used by anyone else.
            commandBindings.Add<CommandErrorEventArgs>(OnCommandErrorHandler) // viewModel.CommandError -> OnCommandErrorHandler
                .AddSource(viewModel, nameof(viewModel.CommandError));
            commandBindings.Add<EventArgs>(OnRebindCommand) // this.Load/viewModel.PropertyChanged -> OnRebindCommand
                .AddSource(this, nameof(Load))
                .AddSource(viewModel, nameof(MainViewModel.PropertyChanged));
            commandBindings.Add(() => { }).AddSource(grid, nameof(grid.DataError)); // grid.DataError: adding an empty handler so no dialogs will be popped up endlessly on errors
            commandBindings.Add<EventArgs>(OnListBindingSourceCurrentItemChangedCommand) // listBindingSource.CurrentItemChanged -> OnListBindingSourceCurrentItemChangedCommand
                .AddSource(listBindingSource, nameof(listBindingSource.CurrentItemChanged));
            commandBindings.Add(OnResetBindingCommand).AddSource(btnReset, nameof(btnReset.Click)); // btnReset.Click -> OnResetBindingCommand

            // even static events are supported, provide a type as source:
            commandBindings.Add<ThreadExceptionEventArgs>(OnApplicationThreadExceptionCommand) // Application.ThreadException -> OnApplicationThreadExceptionCommand
                .AddSource(typeof(Application), nameof(Application.ThreadException));
        }

        #endregion

        #region Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                commandBindings.Dispose(); // since we use only commands for all events, here we release all subscriptions at once
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods

        private void SetProviderPaddings(int padding, params Control[] controls)
        {
            foreach (Control control in controls)
            {
                errorProvider.SetIconPadding(control, padding);
                warningProvider.SetIconPadding(control, padding);
                infoProvider.SetIconPadding(control, padding);
            }
        }

        private void DoRebind()
        {
            // removing possible previous bindings for formatting validation results as colors (would not be needed if elements were always IValidatingObject instances)
            if (formatColorBinding != null)
            {
                tbIntPropList.DataBindings.Remove(intPropColorBinding);
                tbStringPropList.DataBindings.Remove(stringPropColorBinding);
                tbIntPropList.BackColor = SystemColors.Window;
                tbStringPropList.BackColor = SystemColors.Window;
                tbIntPropCurrent.BackColor = SystemColors.Window;
                tbStringPropCurrent.BackColor = SystemColors.Window;
                commandBindings.Remove(formatColorBinding);
                formatColorBinding = null;
            }

            // the actual rebind
            IList testList = viewModel.TestList;
            listBindingSource.SuspendBinding();
            listBindingSource.DataSource = testList;
            listBindingSource.ResumeBinding();
            errorProvider.UpdateBinding();

            // bindings for TextBox.BackColor (could be in the constructor if elements were always IValidatingObject instances but WinForms is not tolerant for invalid property names)
            if (testList.Cast<ITestObject>().FirstOrDefault() is IValidatingObject)
            {
                intPropColorBinding = new Binding(nameof(TextBox.BackColor), listBindingSource, nameof(IValidatingObject.ValidationResults), true, DataSourceUpdateMode.Never);
                stringPropColorBinding = new Binding(nameof(TextBox.BackColor), listBindingSource, nameof(IValidatingObject.ValidationResults), true, DataSourceUpdateMode.Never);
                formatColorBinding = commandBindings.Add<ConvertEventArgs>(OnFormatColor)
                    .AddSource(intPropColorBinding, nameof(Binding.Format))
                    .AddSource(stringPropColorBinding, nameof(Binding.Format));
                tbIntPropList.DataBindings.Add(intPropColorBinding);
                tbStringPropList.DataBindings.Add(stringPropColorBinding);
            }
        }

        #endregion

        #region Command Handlers

        // Note that unlike in Model and ViewModel there are no explicit ICommand definitions for these handlers.
        // For such private commands the CommandBindingsCollection.Add overloads support implicit command initialization from delegates.

        private void OnResetBindingCommand() => listBindingSource.ResetBindings(false);

        private void OnRebindCommand(ICommandSource source)
        {
            if (source.EventArgs is PropertyChangedEventArgs propertyChanged && propertyChanged.PropertyName != nameof(viewModel.TestList))
                return;
            Action rebind = DoRebind;
            if (InvokeRequired)
                Invoke(rebind);
            else
                rebind.Invoke();
        }

        private void OnFormatColor(ICommandSource<ConvertEventArgs> src)
        {
            if (!(src.EventArgs.Value is ValidationResultsCollection results))
                return;
            var bindingValidationResults = (Binding)src.Source;
            var bindingText = bindingValidationResults.Control?.DataBindings[nameof(Control.Text)];
            string propertyName = bindingText?.BindingMemberInfo.BindingField;
            src.EventArgs.Value = results.Errors.Any(r => r.PropertyName == propertyName) ? Color.LightPink
                : results.Warnings.Any(r => r.PropertyName == propertyName) ? Color.Khaki
                : results.Infos.Any(r => r.PropertyName == propertyName) ? Color.LightBlue
                : SystemColors.Window;
        }

        private void OnListBindingSourceCurrentItemChangedCommand(ICommandSource src)
        {
            var bindingSource = (BindingSource)src.Source;

            // adjusting the enabled state of the command bindings using selected index
            commandsWithCurrentItemState.Enabled = bindingSource.Position >= 0;

            // setting the data source of the itemBindingSource
            object current = bindingSource.Current;
            itemBindingSource.DataSource = current ?? typeof(ITestObject);

            // here we could set the data source of an additional set of error/warning/info providers the current item
        }

        private void OnCommandErrorHandler(ICommandSource<CommandErrorEventArgs> source)
        {
            MessageBox.Show($"Operation '{source.EventArgs.Operation}' failed: {source.EventArgs.Exception.Message}{Environment.NewLine}Press Reset to refresh the binding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            source.EventArgs.Handled = true;
        }

        private void OnApplicationThreadExceptionCommand(ICommandSource<ThreadExceptionEventArgs> source) => 
            MessageBox.Show($"An unhandled exception has been detected, which would crash a regular application. Press Reset to update a possibly inconsistent binding.{Environment.NewLine}{Environment.NewLine}"
                + $"The caught exception: {source.EventArgs.Exception}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

        #endregion

        #endregion
    }
}