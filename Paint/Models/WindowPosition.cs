using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    public class WindowPosition
    {
        public double Left;
        public double Top;
        public double Width;
        public double Height;

        public WindowPosition Clone()
        {
            return new WindowPosition
            {
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height
            };
        }
    }
}
