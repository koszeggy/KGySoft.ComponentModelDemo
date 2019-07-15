using System;
using System.Windows;
using KGySoft.Collections;

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    internal static class UIElementExtensions
    {
        // not a ConcurrentDictionary because we don't expect many threads actually
        private static readonly LockingDictionary<FrameworkElement, EventHandler> disposedHandlers = new LockingDictionary<FrameworkElement, EventHandler>();

        /// <summary>
        /// Registers a <paramref name="handler"/> to be executed when the parent Window of <paramref name="element"/> is disposed.
        /// </summary>
        internal static void RegisterOnDisposed(this FrameworkElement element, EventHandler handler)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            AddHandler(element, handler);
            if (element.IsLoaded && Window.GetWindow(element) is Window window)
                HookWindowClosed(element, window);
            else
                element.Loaded += FrameworkElement_Loaded;
        }

        private static void AddHandler(FrameworkElement frameworkElement, EventHandler handler)
        {
            disposedHandlers.Lock();
            if (disposedHandlers.TryGetValue(frameworkElement, out var existingHandler) && existingHandler != null)
                disposedHandlers[frameworkElement] = existingHandler + handler;
            else
                disposedHandlers[frameworkElement] = handler;
            disposedHandlers.Unlock();
        }

        private static void RemoveHandlers(FrameworkElement frameworkElement) => disposedHandlers.Remove(frameworkElement);

        private static void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.Loaded -= FrameworkElement_Loaded;
            if (Window.GetWindow(element) is Window window)
                HookWindowClosed(element, window);
            else
                RemoveHandlers(element); // could not subscribe - removing handler to prevent leaks
        }

        private static void HookWindowClosed(FrameworkElement element, Window window)
        {
            window.Closed += (sender, args) =>
            {
                if (disposedHandlers.TryGetValue(element, out EventHandler handler))
                    handler.Invoke(element, args);
                RemoveHandlers(element);
            };
        }
    }
}
