using Paint.Gestures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Paint.CustomControl
{
    public class DesignItemsControl : ItemsControl
    {
        private Selecting _selectingGesture { get; set; }

        public bool IsDrawing { get; set; }

        static DesignItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignItemsControl), new FrameworkPropertyMetadata(typeof(DesignItemsControl)));
        }

        public DesignItemsControl()
        {
            _selectingGesture = new Selecting(this);
        }

        // Selection Rectngle
        public static readonly DependencyProperty SelectionRectangleProperty =
             DependencyProperty.Register(
                 "SelectionRectangle",
                 typeof(Rect),
                 typeof(DesignItemsControl),
                 new UIPropertyMetadata(null)
        );

        public Rect SelectionRectangle
        {
            get { return (Rect)GetValue(SelectionRectangleProperty); }
            set { SetValue(SelectionRectangleProperty, value); }
        }


        // Selecting
        public static readonly DependencyProperty IsSelectingProperty =
             DependencyProperty.Register(
                 "IsSelecting",
                 typeof(bool),
                 typeof(DesignItemsControl),
                 new UIPropertyMetadata(false)
        );

        public bool IsSelecting
        {
            get { return (bool)GetValue(IsSelectingProperty); }
            set { SetValue(IsSelectingProperty, value); }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DesignItemContainer;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DesignItemContainer
            {
                RenderTransform = new TransformGroup
                {
                    Children = new TransformCollection(
                        new Transform[]
                        {
                            new ScaleTransform(),
                            new TranslateTransform()
                        })
                }
            };

        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (!IsDrawing)
            {
                IsSelecting = true;
                _selectingGesture.OnMouseDownStartSelect(e.GetPosition(this));
                CaptureMouse();
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (IsSelecting)
            {
                _selectingGesture.OnMouseMoveWhenSelecting(e.GetPosition(this));
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            IsSelecting = false;
            ReleaseMouseCapture();
        }
    }
}