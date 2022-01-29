using Paint.AdornerTemplate;
using Paint.CustomControl;
using Paint.Thumb;
using Paint.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint.Adorner
{
    public class LineAdorner : System.Windows.Documents.Adorner
    {
        private DesignItemContainer _container;
        private LineNodeViewModel _nodeVM;

        private LineStartPointThumb _startThumb;
        private LineEndPointThumb _endThumb;

        private VisualCollection _visuals;

        public LineAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _visuals = new VisualCollection(this);
            DataContext = adornedElement;

            _container = (DesignItemContainer)adornedElement;
            _nodeVM = (LineNodeViewModel)_container.DataContext;

            _startThumb = new LineStartPointThumb();
            _visuals.Add(_startThumb);

            _endThumb = new LineEndPointThumb();
            _visuals.Add(_endThumb);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Rect startPointThumbRect = new Rect
            (
                _nodeVM.X1 - _startThumb.Width / 2,
                _nodeVM.Y1 - _startThumb.Height / 2,
                _startThumb.Width,
                _startThumb.Height
            );

            Debug.WriteLine("NodeVM");
            Debug.WriteLine(_nodeVM.Top + " " + _nodeVM.Left + " " + _nodeVM.Width);
            Debug.WriteLine(startPointThumbRect.X + " " + startPointThumbRect.Y);

            _startThumb.Arrange(startPointThumbRect);

            Rect endPointThumbRect = new Rect
            (
                _nodeVM.X2 - _endThumb.Width / 2,
                _nodeVM.Y2 - _endThumb.Height / 2,
                _endThumb.Width,
                _endThumb.Height
            );

            _endThumb.Arrange(endPointThumbRect);

            return arrangeBounds;
        }
    }
}
