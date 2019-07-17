#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using KGySoft.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWinForms.Components
{
    // If you want your ErrorProvider to provide Warning or Information messages of an IValidatingObject (see also Model.ValidatingTestObject),
    // then just drop a ValidationResultToErrorProviderAdapter to the Form and set the Provider, Severity and DataSource properties. See also Forms.MainForm.
    /// <summary>
    /// Provides an adapter for <see cref="ErrorProvider"/> to be able to display validation results of any <see cref="ValidationSeverity"/>.
    /// </summary>
    public sealed class ValidationResultToErrorProviderAdapter : Component
    {
        #region Fields

        private ErrorProvider provider;
        private ValidationSeverity severity;
        private object dataSource;
        private BindingManagerBase currentManager;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the data source that this adapter monitors.
        /// </summary>
        [DefaultValue(null)]
        [AttributeProvider(typeof(IListSource))]
        public object DataSource
        {
            get => dataSource;
            set
            {
                if (dataSource == value)
                    return;
                dataSource = value;
                RewireEvents();
            }
        }

        /// <summary>
        /// Gets or sets the wrapped provider.
        /// </summary>
        [DefaultValue(null)]
        [Description("Select the ErrorProvider instance to wrap by this adapter.")]
        public ErrorProvider Provider
        {
            get => provider;
            set
            {
                if (provider == value)
                    return;
                provider = value;
                if (provider.DataSource != null)
                {
                    DataSource = provider.DataSource;
                    provider.DataSource = null;
                }

                RewireEvents();
            }
        }

        /// <summary>
        /// Gets or sets the severity to be displayed by this adapter.
        /// </summary>
        [Description("The severity of the messages to be displayed by this adapter.")]
        public ValidationSeverity Severity
        {
            get => severity;
            set
            {
                if (severity == value)
                    return;
                severity = value;
                ApplyMessagesFromBinding();
            }
        }

        #endregion

        #region Constructors

        public ValidationResultToErrorProviderAdapter(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            container.Add(this);
        }

        #endregion

        #region Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (currentManager != null)
            {
                UnwireEvents(currentManager);
                currentManager = null;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods

        private void RewireEvents()
        {
            if (currentManager != null)
            {
                UnwireEvents(currentManager);
                currentManager = null;
            }

            BindingManagerBase bindingManager = provider?.ContainerControl?.BindingContext?[dataSource, provider.DataMember];
            if (bindingManager == null)
                return;

            bindingManager.CurrentChanged += BindingManager_CurrentChanged;
            bindingManager.BindingComplete += BindingManager_BindingComplete;
            if (bindingManager is CurrencyManager currencyManager)
            {
                currencyManager.ItemChanged += CurrencyManager_ItemChanged;
                currencyManager.Bindings.CollectionChanged += CurrencyManager_BindingsCollectionChanged;
            }

            currentManager = bindingManager;

            ApplyMessagesFromBinding();
        }

        private void ApplyMessagesFromBinding()
        {
            BindingManagerBase bindingManager = currentManager;
            if (bindingManager == null || bindingManager.Count == 0)
                return;

            BindingsCollection bindings = bindingManager.Bindings;
            object currentItem = bindingManager.Position < 0 ? null : bindingManager.Current;

            // Collecting the messages for the controls
            Dictionary<Control, StringBuilder> controlMessages = new Dictionary<Control, StringBuilder>(bindings.Count);
            foreach (Binding binding in bindings)
            {
                // Ignore everything but bindings to Controls
                var control = binding.Control;
                string propertyName = binding.BindingMemberInfo.BindingField;
                if (control == null || String.IsNullOrEmpty(binding.BindingMemberInfo.BindingField))
                    continue;

                var message = new StringBuilder();
                if (currentItem is IValidatingObject validatingObject)
                {
                    foreach (ValidationResult validationResult in validatingObject.ValidationResults)
                    {
                        if (validationResult.Severity < severity || validationResult.PropertyName != propertyName || String.IsNullOrEmpty(validationResult.Message))
                            continue;

                        if (validationResult.Severity > severity)
                        {
                            message.Clear();
                            break;
                        }

                        if (message.Length > 0)
                            message.AppendLine();
                        message.Append(validationResult.Message);
                    }
                }

                if (!controlMessages.TryGetValue(control, out StringBuilder messages))
                    controlMessages[control] = message;
                else
                {
                    if (messages.Length > 0)
                        messages.AppendLine();
                    messages.Append(message);
                }
            }

            foreach (var entry in controlMessages)
                provider.SetError(entry.Key, entry.Value.ToString());
        }

        private void UnwireEvents(BindingManagerBase manager)
        {
            manager.CurrentChanged -= BindingManager_CurrentChanged;
            manager.BindingComplete -= BindingManager_BindingComplete;
            if (manager is CurrencyManager currencyManager)
            {
                currencyManager.ItemChanged -= CurrencyManager_ItemChanged;
                currencyManager.Bindings.CollectionChanged -= CurrencyManager_BindingsCollectionChanged;
            }
        }

        #endregion

        #region Event handlers

        private void BindingManager_CurrentChanged(object sender, EventArgs e) => ApplyMessagesFromBinding();

        private void BindingManager_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            // showing binding errors only for error severity
            Binding binding = e.Binding;
            if (severity < ValidationSeverity.Error || binding?.Control == null)
                return;
            provider.SetError(binding.Control, e.ErrorText);
        }

        private void CurrencyManager_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            // This is the fixed version of the ErrorProvider.ErrorManager_ItemChanged method.
            var manager = (CurrencyManager)sender;

            // The original handler is "overridden" only due to this part.
            if (e.Index != -1 || manager.Count != 0)
            {
                ApplyMessagesFromBinding();
                return;
            }

            // If the list became empty then reset the messages
            foreach (Binding binding in manager.Bindings)
            {
                if (binding.Control != null)
                    provider.SetError(binding.Control, null);
            }
        }

        private void CurrencyManager_BindingsCollectionChanged(object sender, CollectionChangeEventArgs e) => ApplyMessagesFromBinding();

        #endregion

        #endregion
    }
}
