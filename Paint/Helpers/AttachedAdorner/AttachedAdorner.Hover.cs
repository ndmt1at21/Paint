using Paint.Adorner;
using Paint.CustomControl;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;

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
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
                RectangleAdorner adorner = new RectangleAdorner(element);
                _rectAdorner.Add(adorner);
                layer.Add(adorner);
            }
            else
            {
                var layer = AdornerLayer.GetAdornerLayer(element);
                if (layer != null)
                {
                    foreach (RectangleAdorner adorner in _rectAdorner)
                    {
                        layer.Remove(adorner);
                        _rectAdorner.Clear();
                    }
                }
            }
        }
    }
}
