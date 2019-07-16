#region Usings

using System;
using System.Collections.Concurrent;
using System.Windows;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    // Contains the RegisterOnDisposed extension method for FrameworkElement
    internal static class UIElementExtensions
    {
        #region Fields

        private static readonly ConcurrentDictionary<FrameworkElement, EventHandler> onDisposedHandlers = new ConcurrentDictionary<FrameworkElement, EventHandler>();

        #endregion

        #region Methods

        #region Internal Methods

        /// <summary>
        /// Registers a <paramref name="handler"/> to be executed when the parent <see cref="Window"/> of <paramref name="element"/> is disposed.
        /// <br/>The internal Window.IsDisposed property is not a dependency property so we cannot check its change.
        /// <br/>The private Window.InternalDispose method is executed when the Window is closed so after all we subscribe its Closed event.
        /// </summary>
        internal static void RegisterOnDisposed(this FrameworkElement element, EventHandler handler)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            onDisposedHandlers.AddOrUpdate(element, e => handler, (e, h) => h + handler);
            if (element.IsLoaded && Window.GetWindow(element) is Window window)
                HookWindowClosed(element, window);
            else
                element.Loaded += FrameworkElement_Loaded; // we delay subscribing because we can't get the parent window yet.
        }

        #endregion

        #region Private Methods

        private static void RemoveHandlers(FrameworkElement element) => onDisposedHandlers.TryRemove(element, out var _);

        private static void HookWindowClosed(FrameworkElement element, Window window)
        {
            window.Closed += (sender, args) =>
            {
                if (onDisposedHandlers.TryGetValue(element, out EventHandler handler))
                    handler.Invoke(element, args);
                RemoveHandlers(element);
            };
        }

        #endregion

        #region Event handlers

        private static void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.Loaded -= FrameworkElement_Loaded;
            if (Window.GetWindow(element) is Window window)
                HookWindowClosed(element, window);
            else
                RemoveHandlers(element); // could not subscribe - removing handler to prevent leaks
        }

        #endregion

        #endregion
    }
}
