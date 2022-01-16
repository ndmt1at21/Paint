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
    public class BRCheckbox : CheckBox
    {
        public static readonly DependencyProperty BorderColorProperty =
           DependencyProperty.Register("BorderColor", typeof(Brush), typeof(BRCheckbox));

        public static readonly DependencyProperty BorderHoverColorProperty =
            DependencyProperty.Register("BorderHoverColor", typeof(Brush), typeof(BRCheckbox));

        public static readonly DependencyProperty BorderCheckedColorProperty =
            DependencyProperty.Register("BorderCheckedColor", typeof(Brush), typeof(BRCheckbox));

        public static readonly DependencyProperty BackgroundCheckedProperty =
            DependencyProperty.Register("BackgroundChecked", typeof(SolidColorBrush), typeof(BRCheckbox));

        public static readonly DependencyProperty CheckerColorProperty =
            DependencyProperty.Register("CheckerColor", typeof(SolidColorBrush), typeof(BRCheckbox));

        public static readonly DependencyProperty BorderPressedColorProperty =
            DependencyProperty.Register("BorderPressedColor", typeof(SolidColorBrush), typeof(BRCheckbox));

        public Brush BorderColor
        {
            get => (Brush)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Brush BorderHoverColor
        {
            get => (Brush)GetValue(BorderHoverColorProperty);
            set => SetValue(BorderHoverColorProperty, value);
        }

        public Brush BorderCheckedColor
        {
            get => (Brush)GetValue(BorderCheckedColorProperty);
            set => SetValue(BorderCheckedColorProperty, value);
        }

        public SolidColorBrush BackgroundChecked
        {
            get => (SolidColorBrush)GetValue(BackgroundCheckedProperty);
            set => SetValue(BackgroundCheckedProperty, value);
        }

        public SolidColorBrush CheckerColor
        {
            get => (SolidColorBrush)GetValue(CheckerColorProperty);
            set => SetValue(CheckerColorProperty, value);
        }

        public SolidColorBrush BorderPressedColor
        {
            get => (SolidColorBrush)GetValue(BorderPressedColorProperty);
            set => SetValue(BorderPressedColorProperty, value);
        }

        static BRCheckbox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BRCheckbox), new FrameworkPropertyMetadata(typeof(BRCheckbox)));
        }
    }
}
