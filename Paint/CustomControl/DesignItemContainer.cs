using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.CustomControl
{
    public class DesignItemContainer : ContentControl, IGroupable, ISelectable
    {
        static DesignItemContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignItemContainer), new FrameworkPropertyMetadata(typeof(DesignItemContainer)));
        }

        public ScaleTransform? ScaleTransform => RenderTransform is TransformGroup group ? group.Children.OfType<ScaleTransform>().FirstOrDefault() : null;
        public TranslateTransform? TranslateTransform => RenderTransform is TransformGroup group ? group.Children.OfType<TranslateTransform>().FirstOrDefault() : null;
        public RotateTransform? RotateTransform => RenderTransform is TransformGroup group ? group.Children.OfType<RotateTransform>().FirstOrDefault() : null;


        // Apply Transform 
        public static DependencyProperty ApplyTransformProperty =
            DependencyProperty.RegisterAttached(
                "ApplyTransform",
                typeof(Transform),
                typeof(DesignItemContainer),
                new FrameworkPropertyMetadata(default(Transform), OnApplyTransformChanged)
            );

        public static void SetApplyTransform(UIElement element, Transform value)
        {
            element.SetValue(ApplyTransformProperty, value);
        }

        public static Transform GetApplyTransform(UIElement element)
        {
            return (Transform)element.GetValue(ApplyTransformProperty);
        }

        private static void OnApplyTransformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var designItemContainer = (DesignItemContainer)VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(d));
            designItemContainer.ApplyTransform((Transform)e.NewValue);

        }

        public void ApplyTransform(Transform apply)
        {
            if (apply != null)
                RenderTransform = apply.Clone();
        }

        // Host => DesignItemsControl
        public DesignItemsControl? Host => (DesignItemsControl)ItemsControl.ItemsControlFromItemContainer(this);

        // ID
        public static readonly DependencyProperty IDProperty =
                DependencyProperty.Register("ID", typeof(double), typeof(DesignItemContainer));

        public Guid ID
        {
            get { return (Guid)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // zIndex
        public static readonly DependencyProperty ZIndexProperty =
                DependencyProperty.Register("ZIndex", typeof(double), typeof(DesignItemContainer));

        public int ZIndex
        {
            get { return (int)GetValue(ZIndexProperty); }
            set { SetValue(ZIndexProperty, value); }
        }

        // Top
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(DesignItemContainer));

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Left
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(DesignItemContainer));

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Perent Id
        public static readonly DependencyProperty ParentIDProperty =
           DependencyProperty.Register("ParentID", typeof(Guid), typeof(DesignItemContainer));

        public Guid ParentID
        {
            get { return (Guid)GetValue(ParentIDProperty); }
            set { SetValue(ParentIDProperty, value); }
        }

        // Has Any Group 
        public static readonly DependencyProperty IsGroupProperty =
            DependencyProperty.Register("IsGroup", typeof(bool), typeof(DesignItemContainer));

        public bool IsGroup
        {
            get { return (bool)GetValue(IsGroupProperty); }
            set { SetValue(IsGroupProperty, value); }
        }

        // Is Selected
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected",
                                        typeof(bool),
                                        typeof(DesignItemContainer),
                                        new FrameworkPropertyMetadata(false));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}


