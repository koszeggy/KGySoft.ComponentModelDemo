using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using KGySoft.ComponentModel;
using KGySoft.Reflection;

namespace KGySoft.ComponentModelDemo.ViewWpf.EventToCommand
{
    public class ToKGyCommandExtension : MarkupExtension
    {
        /// <summary>
        /// To provide a matching signature for any event handler.
        /// </summary>
        private sealed class SubscriptionInfo<TEventArgs>
            where TEventArgs : EventArgs
        {
            private sealed class CommandSource : ICommandSource<TEventArgs>
            {
                #region Properties

                #region Public Properties

                public object Source { get; set; }
                public string TriggeringEvent { get; set; }
                public TEventArgs EventArgs { get; set; }

                #endregion

                #region Explicitly Implemented Interface Properties

                EventArgs ICommandSource.EventArgs => EventArgs;

                #endregion

                #endregion
            }

            private readonly ToKGyCommandExtension owner;
            private readonly object source;
            private readonly string eventName;
            private ICommandState state;
            private ICommandBinding updateEnabledBinding;

            public SubscriptionInfo(ToKGyCommandExtension owner, object source, string eventName)
            {
                this.owner = owner;
                this.source = source;
                this.eventName = eventName;
                if (source is FrameworkElement element)
                    element.RegisterOnDisposed((s, e) => updateEnabledBinding?.Dispose());
            }

            internal void Execute(object sender, TEventArgs e)
            {
                var src = source as DependencyObject;
                ICommand command = owner.Command ?? (ICommand)owner.CommandIndirect.Evaluate(src);
                ICommandState currentState = owner.State ?? (ICommandState)owner.StateIndirect.Evaluate(src);
                if (source is UIElement element && state != currentState)
                {
                    updateEnabledBinding?.Dispose();
                    state = currentState;
                    updateEnabledBinding = state.CreatePropertyBinding(nameof(state.Enabled), nameof(element.IsEnabled), element);
                }

                object target = owner.Target ?? owner.TargetIndirect.Evaluate(src);
                if (state?.Enabled != false)
                    command.Execute(new CommandSource{ EventArgs = e, Source = source, TriggeringEvent = eventName}, state, target);
            }
        }

        public ICommand Command { get; set; }
        public BindingBase CommandIndirect { get; set; }

        public ICommandState State { get; set; }
        public BindingBase StateIndirect { get; set; }

        public object Target { get; set; }
        public BindingBase TargetIndirect { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target))
                return null;

            var ei = target.TargetProperty as EventInfo;
            if (ei == null)
                throw new NotSupportedException($"TargetProperty {target.TargetProperty?.GetType().Name} expected to be an EventInfo.");

            object source = target.TargetObject;
            return CreateDelegate(source, ei);
        }

        private Delegate CreateDelegate(object source, EventInfo eventInfo)
        {
            MethodInfo invokeMethod = eventInfo.EventHandlerType.GetMethod(nameof(Action.Invoke));
            ParameterInfo[] parameters = invokeMethod?.GetParameters();
            string eventName = eventInfo.Name;

            if (invokeMethod?.ReturnType != typeof(void) || parameters.Length != 2 || parameters[0].ParameterType != typeof(object) || !typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType))
                throw new ArgumentException($"Event '{nameof(eventName)}' does not have regular event handler delegate type.");

            // creating generic info by reflection because the signature must match and EventArgs can vary
            var info = Reflector.CreateInstance(typeof(SubscriptionInfo<>).MakeGenericType(parameters[1].ParameterType),
                this, source, eventName);

            // WPF will do the subscription itself
            return Delegate.CreateDelegate(eventInfo.EventHandlerType, info, nameof(SubscriptionInfo<EventArgs>.Execute));
        }
    }
}
