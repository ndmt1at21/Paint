using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint.ViewModels
{
    public class ShapeNodeViewModel : NodeViewModel
    {
        public Geometry DefiningShape { get; set; } = Geometry.Empty;
    }
}
