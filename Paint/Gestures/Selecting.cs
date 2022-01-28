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

        private bool onePointSelectionHandleByMouseUp { get; set; }

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

        public void OnMouseUpEndSelecting(Point endPosition)
        {
        }

        public bool SelectByMouseDown(MouseButtonEventArgs e)
        {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(_context.DesignCanvas, e.GetPosition(_context.DesignCanvas));
            DesignItemContainer designItem = Utils.Control.GetParentControl<DesignItemContainer>(hitTestResult.VisualHit);

            // Click outside all designItems
            if (designItem == null)
            {
                UnselectAll();
                return false;
            }

            NodeViewModel nodeVM = (NodeViewModel)designItem.DataContext;

            // Selected item is ignored
            if (nodeVM.IsSelected)
            {
                onePointSelectionHandleByMouseUp = true;
                return false;
            }

            // Press Ctrl
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                AddSelectedItem(nodeVM);
            }
            else
            {
                UnselectAll();
                AddSelectedItem(nodeVM);
            }

            onePointSelectionHandleByMouseUp = false;

            return false;
        }

        public bool SelectByMouseUp(MouseButtonEventArgs e)
        {
            if (!onePointSelectionHandleByMouseUp) return false;

            HitTestResult hitTestResult = VisualTreeHelper.HitTest(_context.DesignCanvas, e.GetPosition(_context.DesignCanvas));
            DesignItemContainer designItem = Utils.Control.GetParentControl<DesignItemContainer>(hitTestResult.VisualHit);

            if (designItem == null)
            {
                UnselectAll();
                return false;
            }

            NodeViewModel nodeVM = (NodeViewModel)designItem.DataContext;

            // Not selected item is ignored
            if (!nodeVM.IsSelected)
            {
                return false;
            }

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                RemoveSelectedItem(nodeVM);
            }
            else
            {
                UnselectAll();
                AddSelectedItem(nodeVM);
            }

            return true;
        }

        public void SelectByArea(Rect rect)
        {
        }

        public void UnselectAll()
        {
            foreach (var item in _context.SelectedItems)
            {
                Debug.WriteLine("Unsleected all");
                item.IsSelected = false;
            }
            _context.SelectedItems.Clear();
        }

        public void AddSelectedItem(NodeViewModel vm)
        {
            vm.IsSelected = true;
            _context.SelectedItems.Add(vm);
        }

        public void RemoveSelectedItem(NodeViewModel vm)
        {
            Debug.WriteLine("Remov eone ite,");
            vm.IsSelected = false;
            _context.SelectedItems.Remove(vm);
            Debug.WriteLine(_context.SelectedItems.Count);
        }
    }
}
