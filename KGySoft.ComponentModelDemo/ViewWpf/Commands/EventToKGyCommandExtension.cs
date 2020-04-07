#region Usings

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

using KGySoft.ComponentModel;
using KGySoft.Reflection;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf.Commands
{
    // By the EventToKGyCommand extension you can bind KGy SOFT commands to events in XAML. 
    // Alternatively, you can wrap the KGy SOFT commands into Microsoft commands (see also the KGyCommandAdapter class); however,
    // if you need to pass the event arguments to a command you cannot use Microsoft's commands. Furthermore, for events that have
    // no command support in WPF you can still use this extension.
    // See the MainWindow.xaml for examples.
    /// <summary>
    /// A markup extension for binding events to <see cref="KGySoft.ComponentModel.ICommand"/> instances in XAML.
    /// </summary>
    public class EventToKGyCommandExtension : MarkupExtension
    {
        #region SubscriptionInfo class

        // To provide a matching signature for any event handler.
        private sealed class SubscriptionInfo<TEventArgs>
            where TEventArgs : EventArgs
        {
            #region CommandSource class

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

            #endregion

            #region Fields

            private readonly EventToKGyCommandExtension owner;
            private readonly object source;
            private readonly string eventName;

            private ICommandState commandState;
            private ICommandBinding syncEnabledBinding;

            #endregion

            #region Properties

            private ICommandState State
            {
                get
                {
                    if (commandState != null)
                        return commandState;
                    commandState = (ICommandState)owner.State?.Evaluate(source as DependencyObject);
                    if (commandState != null)
                        InitEnabledSync();
                    return commandState ??= new CommandState();
                }
            }

            #endregion

            #region Constructors

            public SubscriptionInfo(EventToKGyCommandExtension owner, object source, string eventName, ICommandState state)
            {
                this.owner = owner;
                this.source = source;
                this.eventName = eventName;
                commandState = owner.State == null ? new CommandState() : state; // can be null even if there is a binding but the view is not loaded yet
                InitEnabledSync();
                if (source is FrameworkElement element)
                    element.RegisterOnDisposed((s, e) => syncEnabledBinding?.Dispose());
            }

            #endregion

            #region Methods

            #region Internal Methods

            // This will be the actual handler method of the event.
            internal void Execute(object sender, TEventArgs e)
            {
                var src = source as DependencyObject;
                var command = (ICommand)owner.Command.Evaluate(src);
                if (command == null)
                    return;
                ICommandState state = State; // now it will not be null even if binding could not be resolved in constructor
                object parameter = owner.Parameter?.Evaluate(src);
                if (state.Enabled)
                    command.Execute(new CommandSource { EventArgs = e, Source = source, TriggeringEvent = eventName }, state, null, parameter);
            }

            #endregion

            #region Private Methods

            private void InitEnabledSync()
            {
                if (commandState == null || !(source is UIElement element))
                    return;

                // Creating a CommandState.Enabled -> UIElement.IsEnabled binding
                syncEnabledBinding = commandState.CreatePropertyBinding(nameof(commandState.Enabled), nameof(element.IsEnabled), element);
            }

            #endregion

            #endregion
        }

        #endregion

        #region Properties

        /// <summary>
        /// Must resolve to an <see cref="KGySoft.ComponentModel.ICommand"/> instance.
        /// </summary>
        public BindingBase Command { get; set; }

        /// <summary>
        /// If not null, must resolve to a <see cref="ICommandState"/> instance that the <see cref="ICommand.Execute"/> will receive as the <c>state</c> parameter.
        /// <br/>The <see cref="ICommandState.Enabled"/> property is synced with the event source if it has an <see cref="UIElement.IsEnabled"/> property.
        /// </summary>
        public BindingBase State { get; set; }

        /// <summary>
        /// If not null, must resolve to an object that the <see cref="ICommand.Execute"/> will receive as the <c>parameter</c> argument.
        /// <br/>This binding is evaluated every time when the event is invoked.
        /// </summary>
        public BindingBase Parameter { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Command == null)
                throw new InvalidOperationException($"{nameof(Command)} must nut be null.");
            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target))
                return null;

            var ei = target.TargetProperty as EventInfo;
            if (ei == null)
                throw new NotSupportedException($"{nameof(EventToKGyCommandExtension)} can be used for events but {target.TargetProperty?.GetType().Name} is not an EventInfo.");

            return CreateDelegate(target.TargetObject, ei);
        }

        #endregion

        #region Private Methods

        private Delegate CreateDelegate(object source, EventInfo eventInfo)
        {
            MethodInfo invokeMethod = eventInfo.EventHandlerType.GetMethod(nameof(Action.Invoke));
            ParameterInfo[] parameters = invokeMethod?.GetParameters();
            string eventName = eventInfo.Name;

            if (invokeMethod?.ReturnType != typeof(void) || parameters.Length != 2 || parameters[0].ParameterType != typeof(object) || !typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType))
                throw new ArgumentException($"Event '{nameof(eventName)}' does not have regular event handler delegate type.");

            // creating generic info by reflection because the signature must match and the type of EventArgs can vary
            var info = Reflector.CreateInstance(typeof(SubscriptionInfo<>).MakeGenericType(parameters[1].ParameterType),
                    this, source, eventName, State?.Evaluate(source as DependencyObject));

            // WPF will do the subscription itself. We do not keep any explicit reference to the created delegate to prevent leaks
            return Delegate.CreateDelegate(eventInfo.EventHandlerType, info, nameof(SubscriptionInfo<EventArgs>.Execute));
        }

        #endregion

        #endregion
    }
}
