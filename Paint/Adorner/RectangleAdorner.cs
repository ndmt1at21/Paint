using Paint.AdornerTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Adorner
{
    public class Adorner : AdornerWithTemplate
    {
        public Adorner(UIElement adornedElement) :
            base(
                adornedElement,
                new RectangleAdornerTemplate()
            )
        {
        }
    }
}
