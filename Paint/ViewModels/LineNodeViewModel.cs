using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.ViewModels
{
    public class LineNodeViewModel : NodeViewModel
    {
        private double _x1 { get; set; }
        public double X1
        {
            get { return _x1; }
            set { _x1 = value - Left; }
        }

        private double _y1 { get; set; }
        public double Y1
        {
            get { return _y1; }
            set { _y1 = value - Top; }
        }

        private double _x2 { get; set; }
        public double X2
        {
            get { return _x2; }
            set { _x2 = value - Left; }
        }

        private double _y2 { get; set; }
        public double Y2
        {
            get { return _y2; }
            set { _y2 = value - Top; }
        }

        public override NodeViewModel Clone()
        {
            throw new NotImplementedException();
        }
    }
}
