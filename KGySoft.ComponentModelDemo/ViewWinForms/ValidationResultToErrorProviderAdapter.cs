﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KGySoft.ComponentModel;

namespace BindingTest.ViewWinForms
{
    /// <summary>
    /// Provides an adapter for <see cref="ErrorProvider"/> to be able to display validation results of any <see cref="ValidationSeverity"/>.
    /// </summary>
    internal sealed class ValidationResultToErrorProviderAdapter : IDisposable
    {
        private readonly ErrorProvider provider;
        private readonly ValidationSeverity severity;
        private object dataSource;
        private BindingManagerBase currentManager;

        public ValidationResultToErrorProviderAdapter(ErrorProvider provider, ValidationSeverity severity)
        {
            this.provider = provider;
            this.severity = severity;
            ShowBindingErrors = severity == ValidationSeverity.Error;
        }

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

        public bool ShowBindingErrors { get; set; }

        private void RewireEvents()
        {
            if (currentManager != null)
                UnwireEvents(currentManager);

            BindingManagerBase bindingManager = provider.ContainerControl?.BindingContext?[dataSource, provider.DataMember];
            if (bindingManager == null)
                return;

            // removing the originally set event handlers
            UnwireEvents(bindingManager);

            // wiring the fixed event handlers
            bindingManager.CurrentChanged += BindingManager_CurrentChanged;
            bindingManager.BindingComplete += BindingManager_BindingComplete;
            if (bindingManager is CurrencyManager currencyManager)
            {
                currencyManager.ItemChanged += CurrencyManager_ItemChanged;
                currencyManager.Bindings.CollectionChanged += CurrencyManager_BindingsCollectionChanged;
            }

            currentManager = bindingManager;

            // as we are coming from a newly triggered CurrentChanged we let the rewired handler to go
            ApplyMessagesFromBinding();
        }

        private void ApplyMessagesFromBinding()
        {
            BindingManagerBase bindingManager = currentManager;
            if (bindingManager == null || bindingManager.Count == 0)
                return;

            BindingsCollection bindings = bindingManager.Bindings;
            object currentItem = bindingManager.Current;

            // Collecting the messages for the controls
            Dictionary<Control, StringBuilder> controlMessages = new Dictionary<Control, StringBuilder>(bindings.Count);
            foreach (Binding binding in bindings)
            {
                // Ignore everything but bindings to Controls
                var control = binding.Control;
                if (control == null)
                    continue;

                string propertyName = binding.BindingMemberInfo.BindingField;
                string message = currentItem is IValidatingObject validatingObject && !validatingObject.ValidationResults.Any(vr => vr.Severity > severity)
                    ? validatingObject.ValidationResults[propertyName, severity]?.FirstOrDefault()?.Message
                    : null;

                if (!controlMessages.TryGetValue(control, out StringBuilder sb))
                    controlMessages[control] = new StringBuilder(message ?? String.Empty);
                else if (!String.IsNullOrEmpty(message))
                {
                    sb.AppendLine();
                    sb.Append(message);
                }
            }

            foreach (var entry in controlMessages)
                provider.SetError(entry.Key, entry.Value.ToString());
        }

        private void BindingManager_CurrentChanged(object sender, EventArgs e) => ApplyMessagesFromBinding();

        private void BindingManager_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            Binding binding = e.Binding;
            if (!ShowBindingErrors || binding?.Control == null)
                return;
            provider.SetError(binding.Control, e.ErrorText);
        }

        private void CurrencyManager_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            // This is the fixed version of the base.ErrorManager_ItemChanged method.
            var manager = (CurrencyManager)sender;

            // The original handler is overridden only due to this part.
            if (e.Index != -1 || manager.Count != 0)
            {
                ApplyMessagesFromBinding();
                return;
            }

            // If the list became empty then reset the errors
            foreach (Binding binding in manager.Bindings)
            {
                if (binding.Control != null)
                    provider.SetError(binding.Control, null);
            }
        }

        private void CurrencyManager_BindingsCollectionChanged(object sender, CollectionChangeEventArgs e) => ApplyMessagesFromBinding();

        public void Dispose()
        {
            if (currentManager != null)
            {
                UnwireEvents(currentManager);
                currentManager = null;
            }
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
    }
}