using Paint.CustomControl;
using Paint.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Paint.Gestures
{
    public class Selecting
    {
        private DesignItemsControl _context { get; set; }

        private Point _startPosition { get; set; }

        public Selecting(DesignItemsControl context)
        {
            _context = context;
        }

        public void OnMouseDownStartAreaSelect(Point position)
        {
            _startPosition = position;
            _context.SelectionRectangle = new Rect(position.X, position.Y, 0, 0);
        }

        public void OnMouseMoveAreaSelecting(Point endPosition)
        {
            double top = _startPosition.Y;
            double left = _startPosition.X;
            double width = Math.Abs(endPosition.X - _startPosition.X);
            double height = Math.Abs(endPosition.Y - _startPosition.Y);

            if (_startPosition.X < endPosition.X)
                left = _startPosition.X;

            if (_startPosition.X >= endPosition.X)
                left = endPosition.X;

            if (_startPosition.Y < endPosition.Y)
                top = _startPosition.Y;

            if (_startPosition.Y >= endPosition.Y)
                top = endPosition.Y;

            _context.SelectionRectangle = new Rect(left, top, width, height);
        }

        public void UnselectAll()
        {
            foreach (var item in _context.SelectedItems)
            {
                item.IsSelected = false;
            }
            _context.SelectedItems.Clear();
        }

        public void SelectByMouseDown(MouseButtonEventArgs e)
        {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(_context.DesignCanvas, e.GetPosition(_context.DesignCanvas));
       
            if (hitTestResult != null)
            {
                DesignItemContainer designItem = Utils.Control.GetParentControl<DesignItemContainer>(hitTestResult.VisualHit);

                if (designItem != null)
                {
                    Debug.WriteLine("design item not nulll");
                    NodeViewModel nodeVM = (NodeViewModel)designItem.DataContext;
                    nodeVM.IsSelected = true;

                    // Press Ctrl
                    if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
                    {
                        Debug.WriteLine("dfhjdhfdjh ctrl");
                        _context.SelectedItems.Add(nodeVM);
                    }
                    else
                    {
                        UnselectAll();
                        _context.SelectedItems.Add(nodeVM);
                    }
                }


                if (designItem == null)
                {
                    Debug.WriteLine("!jhdfhjhjhdf");
                    UnselectAll();
                }
            }
        }

        public void SelectByArea(Rect rect)
        {
        }
    }
}
