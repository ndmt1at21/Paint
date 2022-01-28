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
using System.Windows.Forms;

namespace Paint.Helpers
{
    public partial class AttachedAdorner
    {
        public static readonly DependencyProperty ShowResizeAdornerProperty =
            DependencyProperty.RegisterAttached(
                "ShowResizeAdorner",
                typeof(bool),
                typeof(AttachedAdorner),
                new FrameworkPropertyMetadata(false, OnShowResizeAdornerChanged)
            );

        public static void SetShowResizeAdorner(UIElement element, bool value)
            => element.SetValue(ShowResizeAdornerProperty, value);

        public static bool GetShowResizeLine(UIElement element)
            => (bool)element.GetValue(ShowResizeAdornerProperty);

        private static void OnShowResizeAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignItemContainer element = (DesignItemContainer)d;
            bool value = (bool)e.NewValue;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            if (value)
            {
                RectangleAdorner adorner = new RectangleAdorner(element);
                layer.Add(adorner);

                _currentAdorner = adorner;
            }

            if (!value)
            {
                if (layer != null && _currentAdorner != null)
                {
                    layer.Remove(_currentAdorner);
                }
            }
        }
    }
}
