using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfScrollLazyLoading
{
    public static class ScrollHelper
    {
        public static readonly DependencyProperty ScrollToBottomProperty = DependencyProperty.RegisterAttached("ScrollToBottom", typeof(ICommand), typeof(ScrollHelper), new FrameworkPropertyMetadata(null, OnScrollToBottomPropertyChanged));
        private static DependencyObject _superStar;

        public static ICommand GetScrollToBottom(DependencyObject ob)
        {
            return (ICommand)ob.GetValue(ScrollToBottomProperty);
        }

        public static void SetScrollToBottom(DependencyObject ob, ICommand value)
        {
            ob?.SetValue(ScrollToBottomProperty, value);
        }

        private static void OnScrollToBottomPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = obj as ScrollViewer;

            if (scrollViewer == null)
            {
                Border border = (Border)VisualTreeHelper.GetChild(obj, 0);
                scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
            }

            if (scrollViewer != null)
            {
                scrollViewer.Loaded += OnScrollViewerLoaded;
                _superStar = obj;
            }
        }

        private static void OnScrollViewerLoaded(object sender, RoutedEventArgs e)
        {
            (sender as ScrollViewer).Loaded -= OnScrollViewerLoaded;

            (sender as ScrollViewer).Unloaded += OnScrollViewerUnloaded;
            (sender as ScrollViewer).ScrollChanged += OnScrollViewerScrollChanged;
        }

        private static void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)
            {
                //var command = GetScrollToBottom(sender as ScrollViewer);
                var command = GetScrollToBottom(_superStar);
                if (command == null || !command.CanExecute(null))
                    return;

                command.Execute(null);
            }
        }

        private static void OnScrollViewerUnloaded(object sender, RoutedEventArgs e)
        {
            (sender as ScrollViewer).Unloaded -= OnScrollViewerUnloaded;
            (sender as ScrollViewer).ScrollChanged -= OnScrollViewerScrollChanged;
        }

    }
}
