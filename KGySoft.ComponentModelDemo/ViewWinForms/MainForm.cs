using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using KGySoft.ComponentModel;
using KGySoft.ComponentModelDemo.Extensions;
using KGySoft.ComponentModelDemo.Model;
using KGySoft.ComponentModelDemo.ViewModel;
using KGySoft.CoreLibraries;
using KGySoft.Reflection;

namespace KGySoft.ComponentModelDemo.ViewWinForms
{
    public partial class MainForm : Form
    {
        private readonly ViewModel.MainViewModel viewModel;
        private readonly CommandBindingsCollection commandBindings = new CommandBindingsCollection();

        // TODO: to designer
        private readonly BindingSource listBindingSource, itemBindingSource;
        private readonly ValidationResultToErrorProviderAdapter warningsAdapter, infosAdapter;

        // These bindings are dynamically removed and re-created so they are stored as fields
        private Binding intPropListColorBinding, stringPropListColorBinding;
        private Binding intPropCurrentColorBinding, stringPropCurrentColorBinding;
        private ICommandBinding formatColorListBinding, formatColorCurrentBinding;

        static MainForm() => Application.ThreadException += (sender, e) =>
            MessageBox.Show($"An unhandled exception has been detected, which would crash a regular application. Press Reset to update a possibly inconsistent binding.{Environment.NewLine}{Environment.NewLine}"
                + $"The caught exception message: {e.Exception.Message}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public MainForm(ViewModel.MainViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;

            errorProvider.Icon = Images.Error;
            warningProvider.Icon = Images.Warning;
            infoProvider.Icon = Images.Information;

            // TODO: to designer
            warningsAdapter = new ValidationResultToErrorProviderAdapter(warningProvider, ValidationSeverity.Warning);
            infosAdapter = new ValidationResultToErrorProviderAdapter(infoProvider, ValidationSeverity.Information);
            listBindingSource = new BindingSource(components);
            itemBindingSource = new BindingSource(components);
            listBindingSource.DataSource = typeof(ITestObject);
            itemBindingSource.DataSource = typeof(ITestObject);
            listBox.DisplayMember = nameof(ITestObject.StringProp);

            // Setting the TextBoxes of current elements, too; though they have a different source so no validation will appear for them
            // without manual setting or another set of providers bound to the itemBindingSource
            SetProviderPaddings(-20, tbIntPropList, tbStringPropList, tbIntPropCurrent, tbStringPropCurrent);

            // listBindingSource -> grid/listBox/errorProvider/tbIntPropList/tbStringPropList/editMenuStrip1
            grid.DataSource = listBindingSource;
            listBox.DataSource = listBindingSource;
            errorProvider.DataSource = listBindingSource;
            warningsAdapter.DataSource = listBindingSource;
            infosAdapter.DataSource = listBindingSource;
            tbIntPropList.DataBindings.Add(nameof(TextBox.Text), listBindingSource, nameof(ITestObject.IntProp));
            tbStringPropList.DataBindings.Add(nameof(TextBox.Text), listBindingSource, nameof(ITestObject.StringProp));
            editMenuStrip.DataBindings.Add(nameof(editMenuStrip.DataSource), listBindingSource, String.Empty);

            // itemBindingSource -> tbIntPropCurrent/tbStringPropCurrent
            tbIntPropCurrent.DataBindings.Add(nameof(TextBox.Text), itemBindingSource, nameof(ITestObject.IntProp));
            tbStringPropCurrent.DataBindings.Add(nameof(TextBox.Text), itemBindingSource, nameof(ITestObject.StringProp));

            // binding radio buttons by KGySoft.ComponentModel binding (regular WinForms binding works strangely for radio buttons)
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

            // this.Load/viewModel.PropertyChanged -> OnRebindCommand
            commandBindings.Add<EventArgs>(OnRebindCommand)
                .AddSource(this, nameof(Load))
                .AddSource(viewModel, nameof(ViewModel.MainViewModel.PropertyChanged));

            // grid.DataError: adding an empty handler so no dialogs will be popped up endlessly on errors
            commandBindings.Add(() => { }).AddSource(grid, nameof(grid.DataError));

            // listBindingSource.CurrentItemChanged -> OnListBindingSourceCurrentItemChangedCommand
            commandBindings.Add<EventArgs>(OnListBindingSourceCurrentItemChangedCommand)
                .AddSource(listBindingSource, nameof(listBindingSource.CurrentItemChanged));

            // btnChangeInner.Checked -> viewModel.ChangeUnderlyingCollection
            commandBindings.AddPropertyBinding(btnChangeInner, nameof(btnChangeInner.Checked), nameof(viewModel.ChangeUnderlyingCollection), viewModel);

            // btnReset.Click -> OnResetBindingCommand
            commandBindings.Add(OnResetBindingCommand)
                .AddSource(btnReset, nameof(btnReset.Click));

            // btnAdd.Click -> OnAddItemCommand
            commandBindings.Add(OnAddItemCommand)
                .AddSource(btnAdd, nameof(btnAdd.Click));

            // btnRemove.Click -> OnRemoveItemCommand
            commandBindings.Add(OnRemoveItemCommand)
                .AddSource(btnRemove, nameof(btnRemove.Click));

            // btnSetItem.Click -> OnSetItemCommand
            commandBindings.Add(OnSetItemCommand)
                .AddSource(btnSetItem, nameof(btnSetItem.Click));

            // btnSetProp.Click -> OnSetItemPropertyCommand
            commandBindings.Add(OnSetItemPropertyCommand)
                .AddSource(btnSetProp, nameof(btnSetProp.Click));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                commandBindings.Dispose();
            }

            base.Dispose(disposing);
        }

        private void SetProviderPaddings(int padding, params Control[] controls)
        {
            foreach (Control control in controls)
            {
                errorProvider.SetIconPadding(control, padding);
                warningProvider.SetIconPadding(control, padding);
                infoProvider.SetIconPadding(control, padding);
            }
        }

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

        private void DoRebind()
        {
            // removing possible previous bindings for formatting validation results as colors (would not be needed if elements were always IValidatingObject instances)
            if (formatColorListBinding != null)
            {
                tbIntPropList.DataBindings.Remove(intPropListColorBinding);
                tbStringPropList.DataBindings.Remove(stringPropListColorBinding);
                tbIntPropCurrent.DataBindings.Remove(intPropCurrentColorBinding);
                tbStringPropCurrent.DataBindings.Remove(stringPropCurrentColorBinding);
                tbIntPropList.BackColor = SystemColors.Window;
                tbStringPropList.BackColor = SystemColors.Window;
                tbIntPropCurrent.BackColor = SystemColors.Window;
                tbStringPropCurrent.BackColor = SystemColors.Window;
                commandBindings.Remove(formatColorListBinding);
                commandBindings.Remove(formatColorCurrentBinding);
                formatColorListBinding = formatColorCurrentBinding = null;
            }

            // the actual rebind
            IList testList = viewModel.TestList;
            listBindingSource.SuspendBinding();
            listBindingSource.DataSource = testList;
            listBindingSource.ResumeBinding();
            errorProvider.UpdateBinding();

            // validation colors below (could be in the constructor if elements were always IValidatingObject instances)
            if (testList.Cast<ITestObject>().FirstOrDefault() is IValidatingObject)
            {
                intPropListColorBinding = new Binding(nameof(TextBox.BackColor), listBindingSource, nameof(IValidatingObject.ValidationResults), true, DataSourceUpdateMode.Never);
                stringPropListColorBinding = new Binding(nameof(TextBox.BackColor), listBindingSource, nameof(IValidatingObject.ValidationResults), true, DataSourceUpdateMode.Never);
                formatColorListBinding = commandBindings.Add<ConvertEventArgs>(OnFormatColor)
                    .AddSource(intPropListColorBinding, nameof(Binding.Format))
                    .AddSource(stringPropListColorBinding, nameof(Binding.Format));
                tbIntPropList.DataBindings.Add(intPropListColorBinding);
                tbStringPropList.DataBindings.Add(stringPropListColorBinding);

                intPropCurrentColorBinding = new Binding(nameof(TextBox.BackColor), itemBindingSource, nameof(IValidatingObject.ValidationResults), true, DataSourceUpdateMode.Never);
                stringPropCurrentColorBinding = new Binding(nameof(TextBox.BackColor), itemBindingSource, nameof(IValidatingObject.ValidationResults), true, DataSourceUpdateMode.Never);
                formatColorCurrentBinding = commandBindings.Add<ConvertEventArgs>(OnFormatColor)
                    .AddSource(intPropCurrentColorBinding, nameof(Binding.Format))
                    .AddSource(stringPropCurrentColorBinding, nameof(Binding.Format));
                tbIntPropCurrent.DataBindings.Add(intPropCurrentColorBinding);
                tbStringPropCurrent.DataBindings.Add(stringPropCurrentColorBinding);

            }
        }

        private void OnResetBindingCommand() => listBindingSource.ResetBindings(false);

        private void OnAddItemCommand()
        {
            IList list = (IList)listBindingSource.DataSource;
            list = GetListToModify(list);
            Type elementType = list.GetType().GetInterface(typeof(IList<>).Name).GetGenericArguments()[0];
            try
            {
                list.Add(Reflector.CreateInstance(elementType));
            }
            catch (Exception e)
            {
                HandleError(e);
            }
        }

        private void OnRemoveItemCommand()
        {
            var list = (IList)listBindingSource.DataSource;
            int current = listBindingSource.Position;
            if (current < 0)
            {
                ShowNoSelectedElement();
                return;
            }

            list = GetListToModify(list);
            try
            {
                list.RemoveAt(current);
            }
            catch (Exception e)
            {
                HandleError(e);
            }
        }

        private void OnSetItemCommand()
        {
            IList list = (IList)listBindingSource.DataSource;
            int current = listBindingSource.Position;
            if (current < 0)
            {
                ShowNoSelectedElement();
                return;
            }

            list = GetListToModify(list);
            Type elementType = list.GetType().GetInterface(typeof(IList<>).Name).GetGenericArguments()[0];
            try
            {
                list[current] = viewModel.RandomInstance.NextObject(elementType);
            }
            catch (Exception e)
            {
                HandleError(e);
            }
        }

        private void OnSetItemPropertyCommand()
        {
            IList list = (IList)listBindingSource.DataSource;
            int current = listBindingSource.Position;
            if (current < 0)
            {
                ShowNoSelectedElement();
                return;
            }

            var item = (ITestObject)list[current];
            try
            {
                item.StringProp = viewModel.RandomInstance.NextString();
            }
            catch (Exception e)
            {
                HandleError(e);
            }
        }

        private IList GetListToModify(IList list)
        {
            if (!viewModel.ChangeUnderlyingCollection)
                return list;

            PropertyInfo itemsProp = list.GetType().GetProperty("Items", BindingFlags.Instance | BindingFlags.NonPublic);
            return itemsProp == null ? list : (IList)itemsProp.GetValue(list, null);
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
            object current = bindingSource.Current;
            itemBindingSource.DataSource = current ?? typeof(ITestObject);
            // here we could set the data source of an additional error/warning/info provider set for the current item
        }

        private static void ShowNoSelectedElement() 
            => MessageBox.Show("No selected element", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private static void HandleError(Exception e, [CallerMemberName]string operation = null) 
            => MessageBox.Show($"Operation '{operation}' failed: {e.Message}{Environment.NewLine}Press Reset to refresh the binding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
