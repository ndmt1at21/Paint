using Paint.CustomControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Gestures
{
    public class Drawing
    {
        private DesignItemsControl _context { get; set; }
        private Point _startPosition { get; set; }

        public Drawing(DesignItemsControl itemsControl)
        {
            _context = itemsControl;
        }

        public void OnMouseDown(Point startPosition)
        {
            _startPosition = startPosition;
            Debug.WriteLine("startDrawiing");
            _context.DrawingNode.IsDrawing = true;
        }

        public void OnMouseMove(Point movingPosition)
        {
            double top = _startPosition.Y;
            double left = _startPosition.X;
            double width = Math.Abs(movingPosition.X - _startPosition.X);
            double height = Math.Abs(movingPosition.Y - _startPosition.Y);

            if (_startPosition.X < movingPosition.X)
                left = _startPosition.X;

            if (_startPosition.X >= movingPosition.X)
                left = movingPosition.X;

            if (_startPosition.Y < movingPosition.Y)
                top = _startPosition.Y;

            if (_startPosition.Y >= movingPosition.Y)
                top = movingPosition.Y;

            _context.DrawingNode.Top = top;
            _context.DrawingNode.Left = left;
            _context.DrawingNode.Width = width;
            _context.DrawingNode.Height = height;
        }

        public void OnMouseUp(Point endPosition)
        {
            _context.DrawingNode.IsSelected = true;
            _context.DrawingNode.IsDrawing = false;
            _context.SelectedItems.Add(_context.DrawingNode);
            _context.DrawingNode = null;
        }
    }
}
