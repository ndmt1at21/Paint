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

            if (value)
            {
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
                Adorner.Adorner adorner = new Adorner.Adorner(element);
                _rectAdorner.Add(adorner);
                layer.Add(adorner);
            }
            else
            {
                var layer = AdornerLayer.GetAdornerLayer(element);
                if (layer != null)
                {
                    foreach (Adorner.Adorner adorner in _rectAdorner)
                    {
                        layer.Remove(adorner);
                        _rectAdorner.Clear();
                    }
                }
            }
        }

        // Rectangle Adorner
        private static void OnShowResizeLineAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        // Line Adorner
        private static void Show(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
