using Paint.Gestures;
using Paint.ViewModels;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace Paint.CustomControl
{
    public class DesignItemsControl : ItemsControl
    {
        private Selecting _selectingGesture { get; set; }
        private Gestures.Drawing _drawingGesture;

        public DesignCanvas DesignCanvas { get; set; }

        public bool IsDrawing { get; set; }
        public bool IsDragging { get; set; }

        static DesignItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignItemsControl), new FrameworkPropertyMetadata(typeof(DesignItemsControl)));
        }

        public DesignItemsControl()
        {
            DraggingAttached.Context = this;
            _selectingGesture = new Selecting(this);
            _drawingGesture = new Gestures.Drawing(this);
        }

        // Curent Node for drawing
        public static readonly DependencyProperty DrawingNodeProperty =
            DependencyProperty.Register(
                "DrawingNode",
                typeof(NodeViewModel),
                typeof(DesignItemsControl),
                new UIPropertyMetadata(null)
       );

        public NodeViewModel DrawingNode
        {
            get { return (NodeViewModel)GetValue(DrawingNodeProperty); }
            set { SetValue(DrawingNodeProperty, value); }
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

        // Selected Items
        public static readonly DependencyProperty SelectedItemsProperty =
             DependencyProperty.Register(
                 "SelectedItems",
                 typeof(ObservableCollection<NodeViewModel>),
                 typeof(DesignItemsControl),
                 new UIPropertyMetadata(new ObservableCollection<NodeViewModel>())
        );

        public ObservableCollection<NodeViewModel> SelectedItems
        {
            get { return (ObservableCollection<NodeViewModel>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
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

            EventAnalyze(e);

            _selectingGesture.SelectByMouseDown(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            //if (IsSelecting && !DragManager.IsDragging)
            //{
            //    _selectingGesture.OnMouseMoveAreaSelecting(e.GetPosition(this));
            //}

            //if (IsDrawing)
            //{
            //    _drawingGesture.OnMouseMove(e.GetPosition(this));
            //}
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            Debug.WriteLine("On Mouse Left");
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (IsSelecting)
            {
                IsSelecting = false;
                ReleaseMouseCapture();
            }

            if (IsDrawing)
            {
                _drawingGesture.OnMouseUp(e.GetPosition(this));
                IsDrawing = false;
            }

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DesignCanvas = (DesignCanvas)GetTemplateChild("PART_Canvas");
        }

        private void EventAnalyze(MouseButtonEventArgs e)
        {
            if (DrawingNode != null)
            {
                IsDrawing = true;
            }


            Debug.WriteLine("IsDrawing " + IsDrawing + "; IsSelecting " + IsSelecting);
            //if (IsSelecting)
            //{
            //    IsSelecting = false;
            //}

            //Debug.WriteLine("On Preview Mouse Left");
        }
    }
}