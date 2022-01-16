using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BRControl
{
    public class BRInputText : TextBox
    {
        public static readonly DependencyProperty CornerRadiusProperty =
                DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TextBox));

        public static readonly DependencyProperty PlaceholderProperty =
               DependencyProperty.Register("Placeholder", typeof(string), typeof(TextBox));

        public static readonly DependencyProperty PlaceholderColorProperty =
              DependencyProperty.Register("PlaceholderColor", typeof(SolidColorBrush), typeof(TextBox));

        public static readonly DependencyProperty StatusLineColorProperty =
           DependencyProperty.Register("StatusLineColor", typeof(SolidColorBrush), typeof(TextBox));

        public static readonly DependencyProperty StatusLineFocusedColorProperty =
         DependencyProperty.Register("StatusLineFocusedColor", typeof(SolidColorBrush), typeof(TextBox));

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(SolidColorBrush), typeof(TextBox));

        public static readonly DependencyProperty FocusedBackgroundProperty =
            DependencyProperty.Register("FocusedBackground", typeof(SolidColorBrush), typeof(TextBox));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public SolidColorBrush PlaceholderColor
        {
            get => (SolidColorBrush)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public SolidColorBrush StatusLineColor
        {
            get => (SolidColorBrush)GetValue(StatusLineColorProperty);
            set => SetValue(StatusLineColorProperty, value);
        }

        public SolidColorBrush StatusLineFocusedColor
        {
            get => (SolidColorBrush)GetValue(StatusLineFocusedColorProperty);
            set => SetValue(StatusLineFocusedColorProperty, value);
        }

        public SolidColorBrush HoverBackground
        {
            get => (SolidColorBrush)GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }

        public SolidColorBrush FocusedBackground
        {
            get => (SolidColorBrush)GetValue(FocusedBackgroundProperty);
            set => SetValue(FocusedBackgroundProperty, value);
        }

        static BRInputText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BRInputText), new FrameworkPropertyMetadata(typeof(BRInputText)));
        }
    }
}
