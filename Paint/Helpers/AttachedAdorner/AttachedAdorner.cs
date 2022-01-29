using Paint.Adorner;
using Paint.CustomControl;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Paint.Helpers
{
    public partial class AttachedAdorner
    {
        private static System.Windows.Documents.Adorner _currentRectHoverAdorner { get; set; }
        private static System.Windows.Documents.Adorner _currentLineHoverAdorner { get; set; }
    }
}
