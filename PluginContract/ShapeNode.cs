﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PluginContract
{
    public interface IShapeNode : INode
    {
        public string CreateGeometry();
    }
}
