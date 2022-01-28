using Paint.Adorner;
using Paint.CustomControl;
using Paint.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Paint.Helpers
{
    public partial class AttachedAdorner
    {
        // For Rectangle
        public static readonly DependencyProperty HasMoveAdornerProperty =
            DependencyProperty.RegisterAttached(
                "HasMoveAdorner",
                typeof(bool),
                typeof(AttachedAdorner),
                new FrameworkPropertyMetadata(false, OnHasMoveAdornerChanged)
            );

        public static void SetHasMoveAdorner(UIElement element, bool value)
            => element.SetValue(HasMoveAdornerProperty, value);

        public static bool GetHasMoveLine(UIElement element)
            => (bool)element.GetValue(HasMoveAdornerProperty);

        private static void OnHasMoveAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignItemContainer element = (DesignItemContainer)d;
            bool value = (bool)e.NewValue;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
            RectangleMoveAdorner adorner = new RectangleMoveAdorner(element);

            Debug.WriteLine("atduyfyfufydf");
            Debug.WriteLine(value);

            if (value)
            {
                layer.Add(adorner);
                _currentAdorner = adorner;
            }

            if (!value)
            {
                layer.Remove(_currentAdorner);
            }
        }
    }
}
