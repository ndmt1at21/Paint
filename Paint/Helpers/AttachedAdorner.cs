using Paint.Adorner;
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
    public class AttachedAdorner
    {
        // All adorner show on canvas
        private static readonly List<MoveResizeRotateAdorner> _rectAdorner
            = new List<MoveResizeRotateAdorner>();

        public static readonly DependencyProperty ShowResizeAdornerProperty = DependencyProperty.RegisterAttached("ShowResizeAdorner", typeof(bool), typeof(AttachedAdorner),
            new FrameworkPropertyMetadata(false, OnShowResizeAdornerChanged));
        public static void SetShowResizeAdorner(UIElement element, bool value) => element.SetValue(ShowResizeAdornerProperty, value);
        public static bool GetShowResizeAdorner(UIElement element) => (bool)element.GetValue(ShowResizeAdornerProperty);

        private static void OnShowResizeAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContentPresenter element = (ContentPresenter)d;
            bool value = (bool)e.NewValue;

            if (value)
            {
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
                MoveResizeRotateAdorner adorner = new MoveResizeRotateAdorner(element);
                _rectAdorner.Add(adorner);
                layer.Add(adorner);
            }
            else
            {
                var layer = AdornerLayer.GetAdornerLayer(element);
                if (layer != null)
                {
                    foreach (MoveResizeRotateAdorner adorner in _rectAdorner)
                    {
                        layer.Remove(adorner);
                        _rectAdorner.Clear();
                    }
                }
            }
        }
    }
}
