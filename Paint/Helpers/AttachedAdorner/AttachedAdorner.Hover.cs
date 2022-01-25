using Paint.Adorner;
using Paint.CustomControl;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Paint.Helpers
{
    public partial class AttachedAdorner
    {
        // For Rectangle
        public static readonly DependencyProperty HasHoverAdornerProperty =
            DependencyProperty.RegisterAttached(
                "HasHoverAdorner",
                typeof(bool),
                typeof(AttachedAdorner),
                new FrameworkPropertyMetadata(false, OnHasHoverAdornerChanged)
            );

        public static void SetHasHoverAdorner(UIElement element, bool value)
            => element.SetValue(HasHoverAdornerProperty, value);

        public static bool GetHasHoverLine(UIElement element)
            => (bool)element.GetValue(HasHoverAdornerProperty);

        private static void OnHasHoverAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignItemContainer element = (DesignItemContainer)d;
            bool value = (bool)e.NewValue;

            if (value)
            {
                element.MouseEnter += OnMouseEnterRectangle;
                element.MouseLeave += OnMouseLeaveRectangle;
            }
            else
            {
                element.MouseEnter -= OnMouseEnterRectangle;
                element.MouseLeave -= OnMouseLeaveRectangle;
            }
        }

        private static void OnMouseEnterRectangle(object sender, MouseEventArgs e)
        {
            DesignItemContainer element = (DesignItemContainer)sender;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
            RectangleHoverAdorner adorner = new RectangleHoverAdorner(element);

            layer.Add(adorner);
            _currentAdorner = adorner;
        }

        private static void OnMouseLeaveRectangle(object sender, MouseEventArgs e)
        {
            DesignItemContainer element = (DesignItemContainer)sender;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            if (layer != null)
            {
                layer.Remove(_currentAdorner);
            }
        }
    }
}
