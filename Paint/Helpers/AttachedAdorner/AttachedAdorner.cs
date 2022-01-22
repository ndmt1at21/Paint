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
        // All adorner show on canvas
        private static readonly List<System.Windows.Documents.Adorner> _rectAdorner
            = new List<System.Windows.Documents.Adorner>();
    }
}
