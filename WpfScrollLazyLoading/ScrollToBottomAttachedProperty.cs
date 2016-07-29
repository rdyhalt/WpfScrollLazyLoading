using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfScrollLazyLoading
{
    /// <summary>
    /// Build on http://stackoverflow.com/questions/10793717/how-to-find-that-scrollviewer-is-scrolled-to-the-end-in-wpf
    /// Will activate an ICommand when ScrollViewer has reached bottom.
    /// </summary>
    public class ScrollToBottomAttachedProperty
    {
        public static readonly DependencyProperty ScrollToBottomProperty = DependencyProperty.RegisterAttached("ScrollToBottom", typeof(ICommand), typeof(ScrollToBottomAttachedProperty), new FrameworkPropertyMetadata(null, OnScrollToBottomPropertyChanged));
        private static readonly Dictionary<int, DependencyObject> ScrollViewerDependencyObject = new Dictionary<int, DependencyObject>();
        private static readonly Dictionary<int, DependencyObject> FrameworkElementLoading = new Dictionary<int, DependencyObject>();

        public static ICommand GetScrollToBottom(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ScrollToBottomProperty);
        }

        public static void SetScrollToBottom(DependencyObject obj, ICommand value)
        {
            obj?.SetValue(ScrollToBottomProperty, value);
        }

        private static void OnScrollToBottomPropertyChanged(DependencyObject scrollToBottom, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = scrollToBottom as FrameworkElement;
            if (frameworkElement == null) return;

            if (frameworkElement.IsLoaded)
            {
                ScrollViewer findScrollViewer = FindScrollViewer(scrollToBottom);
                if (findScrollViewer != null && ScrollViewerDependencyObject.ContainsKey(findScrollViewer.GetHashCode()) == false)
                {
                    ScrollViewerDependencyObject.Add(findScrollViewer.GetHashCode(), scrollToBottom);
                }
            }
            else
            {
                if (FrameworkElementLoading.ContainsKey(frameworkElement.GetHashCode())) return;
                FrameworkElementLoading.Add(frameworkElement.GetHashCode(), scrollToBottom);
                frameworkElement.Loaded += FrameworkElementLoaded;
            }
        }

        private static void FrameworkElementLoaded(object sender, RoutedEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement == null) return;
            frameworkElement.Loaded -= FrameworkElementLoaded;

            DependencyObject scrollToBottom;
            if (FrameworkElementLoading.TryGetValue(frameworkElement.GetHashCode(), out scrollToBottom))
            {
                FrameworkElementLoading.Remove(frameworkElement.GetHashCode());

                ScrollViewer findScrollViewer = FindScrollViewer((DependencyObject)sender);
                if (findScrollViewer != null && ScrollViewerDependencyObject.ContainsKey(findScrollViewer.GetHashCode()) == false)
                {
                    ScrollViewerDependencyObject.Add(findScrollViewer.GetHashCode(), scrollToBottom);
                }
            }
        }

        private static ScrollViewer FindScrollViewer(DependencyObject obj)
        {
            var scrollViewer = obj as ScrollViewer;

            if (scrollViewer == null && VisualTreeHelper.GetChildrenCount(obj) > 0)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, 0);

                if (child is Border)
                {
                    scrollViewer = VisualTreeHelper.GetChild(child, 0) as ScrollViewer;
                }
                else if (child is ScrollViewer)
                {
                    scrollViewer = child as ScrollViewer;
                }
            }

            if (scrollViewer != null && scrollViewer.IsLoaded)
            {
                scrollViewer.Unloaded += OnScrollViewerUnloaded;
                scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            }

            return scrollViewer;
        }

        private static void OnScrollViewerUnloaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null) return;

            scrollViewer.Unloaded -= OnScrollViewerUnloaded;
            scrollViewer.ScrollChanged -= OnScrollViewerScrollChanged;
        }

        /// <summary>
        /// Call ICommand when ScrollView is at the bottom.
        /// </summary>
        private static void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            if (scrollViewer?.VerticalOffset >= scrollViewer?.ScrollableHeight)
            {
                DependencyObject dependencyObject;
                if (ScrollViewerDependencyObject.TryGetValue(scrollViewer.GetHashCode(), out dependencyObject))
                {
                    var command = GetScrollToBottom(dependencyObject);
                    if (command == null || command.CanExecute(null) == false) return;
                    command.Execute(null);
                }
            }
        }
    }
}
