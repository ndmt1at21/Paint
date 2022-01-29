using Paint.Adorner;
using Paint.CustomControl;
using Paint.ViewModels;
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
            NodeViewModel nodeVM = (NodeViewModel)element.DataContext;

            bool value = (bool)e.NewValue;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            if (value)
            {
                if (nodeVM is LineNodeViewModel)
                    HandleAddResizeLineAdorner(element, layer);
                else
                    HandleAddResizeRectangleAdorner(element, layer);
            }

            if (!value)
            {
                if (nodeVM is LineNodeViewModel)
                    HandleRemoveResizeLineAdorner(element, layer);
                else 
                    HandleRemoveResizeRectangleAdorner(element, layer);
            }
        }

        private static void HandleAddResizeRectangleAdorner(DesignItemContainer element, AdornerLayer layer)
        {
            RectangleAdorner adorner = new RectangleAdorner(element);
            layer.Add(adorner);
        }

        private static void HandleRemoveResizeRectangleAdorner(DesignItemContainer element, AdornerLayer layer)
        {
            if (layer == null) return;

            System.Windows.Documents.Adorner[] adorners = layer.GetAdorners(element);
            foreach (System.Windows.Documents.Adorner adorner in adorners)
            {
                layer.Remove(adorner);
            }
        }

        private static void HandleAddResizeLineAdorner(DesignItemContainer element, AdornerLayer layer)
        {
            LineAdorner adorner = new LineAdorner(element);
            layer.Add(adorner);
        }

        private static void HandleRemoveResizeLineAdorner(DesignItemContainer element, AdornerLayer layer)
        {
            if (layer == null) return;

            System.Windows.Documents.Adorner[] adorners = layer.GetAdorners(element);
            foreach (System.Windows.Documents.Adorner adorner in adorners)
            {
                layer.Remove(adorner);
            }
        }
    }
}
