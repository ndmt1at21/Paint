using Paint.CustomControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Paint.Gestures
{
    internal static class DraggingAttached
    {
        public delegate void DragDeltaEventHandler(DesignItemContainer sender, Point point);
        public static event DragDeltaEventHandler DragDelta;

        public static DesignItemsControl Context { get; set; }

        public static bool IsPossibleDragging { get; private set; }
        public static DependencyProperty IsPossibleDraggingProperty = DependencyProperty.RegisterAttached
            (
                "IsPossibleDragging",
                typeof(bool),
                typeof(DesignItemContainer),
                new PropertyMetadata(OnIsPossibleDraggingChanged)
            );

        public static void SetIsPossibleDragging(UIElement element, bool value) => element.SetValue(IsPossibleDraggingProperty, value);
        public static bool GetIsPossibleDragging(UIElement element) => (bool)element.GetValue(IsPossibleDraggingProperty);

        private static Point _initialPosition;

        private static void OnIsPossibleDraggingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignItemContainer container = (DesignItemContainer)d;

            if (container != null)
            {
                bool IsPossibleDragging = (bool)e.NewValue;

                if (IsPossibleDragging)
                {
                    container.MouseDown += OnSelectedContainerClicked;
                    container.MouseMove += OnSelectedContainerMove;
                    container.MouseUp += OnSelectedContainerReleased;
                }
                else
                {
                    container.MouseDown -= OnSelectedContainerClicked;
                    container.MouseMove -= OnSelectedContainerMove;
                    container.MouseUp -= OnSelectedContainerReleased;
                }
            }
        }

        private static void OnSelectedContainerClicked(object sender, MouseButtonEventArgs e)
        {
            _initialPosition = e.GetPosition(Context.DesignCanvas);
            var container = (DesignItemContainer)sender;
            container.CaptureMouse();
        }

        private static void OnSelectedContainerMove(object sender, MouseEventArgs e)
        {
            var container = (DesignItemContainer)sender;

            if (container.IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(Context.DesignCanvas);

                if (_initialPosition == currentPosition) return;

                Debug.WriteLine((currentPosition.X - _initialPosition.X) + " " + (currentPosition.Y - _initialPosition.Y));
                DragDelta?.Invoke((DesignItemContainer)sender, new Point(currentPosition.X - _initialPosition.X, currentPosition.Y - _initialPosition.Y));
                _initialPosition = currentPosition;
            }
        }

        private static void OnSelectedContainerReleased(object sender, MouseButtonEventArgs e)
        {
            var container = (DesignItemContainer)sender;
            container.ReleaseMouseCapture();
            IsPossibleDragging = false;
        }
    }
}
