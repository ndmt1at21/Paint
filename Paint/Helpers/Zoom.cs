using Paint.CustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint.Helpers
{
    public class Zoom
    {
        //private readonly ScaleTransform _scaleTransform => _contex
        //private TranslateTransform _translateTransform => _context.RenderTransform.Transform
        //private readonly DesignItemsControl _context;

        //internal double MinScale => 1;
        //internal double MaxScale => 10;

        //internal Zoom(DesignItemsControl context)
        //{
        //    _context = context;
        //}

        //internal void ZoomToPosition(Point position, int delta, double factor)
        //{
        //    var previousScaleX = _scaleTransform.ScaleX;
        //    var previousScaleY = _scaleTransform.ScaleY;
        //    var originX = (position.X - _translateTransform.X) / _scaleTransform.ScaleX;
        //    var originY = (position.Y - _translateTransform.Y) / _scaleTransform.ScaleY;

        //    if (delta > 0)
        //    {
        //        _scaleTransform.ScaleY *= factor;
        //        _scaleTransform.ScaleX *= factor;
        //    }
        //    else
        //    {
        //        _scaleTransform.ScaleY /= factor;
        //        _scaleTransform.ScaleX /= factor;
        //    }
        //    if (_scaleTransform.ScaleX <= MinScale)
        //    {
        //        _scaleTransform.ScaleX = MinScale;
        //    }
        //    if (_scaleTransform.ScaleY <= MinScale)
        //    {
        //        _scaleTransform.ScaleY = MinScale;
        //    }
        //    if (_scaleTransform.ScaleX >= MaxScale)
        //    {
        //        _scaleTransform.ScaleX = MaxScale;
        //    }
        //    if (_scaleTransform.ScaleY >= MaxScale)
        //    {
        //        _scaleTransform.ScaleY = MaxScale;
        //    }

        //    if (previousScaleX != _scaleTransform.ScaleX)
        //    {
        //        _translateTransform.X = position.X - originX * _scaleTransform.ScaleX;
        //    }
        //    if (previousScaleY != _scaleTransform.ScaleY)
        //    {
        //        _translateTransform.Y = position.Y - originY * _scaleTransform.ScaleY;
        //    }
        //}

    }
}
