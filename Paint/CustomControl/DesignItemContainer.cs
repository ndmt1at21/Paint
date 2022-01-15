using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Paint.CustomControl
{
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    public class DesignItemContainer : ContentControl, IGroupable, ISelectable
    {
        private const string ContentPresenterName = "PART_ContentPresenter";

        static DesignItemContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignItemContainer), new FrameworkPropertyMetadata(typeof(DesignItemContainer)));
        }

        // Host Container
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


