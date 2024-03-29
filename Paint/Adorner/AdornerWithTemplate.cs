﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Adorner
{
    public class AdornerWithTemplate : System.Windows.Documents.Adorner
    {
        private VisualCollection _visuals;
        private Control _template;

        protected override int VisualChildrenCount
        {
            get
            {
                return _visuals.Count;
            }
        }

        public AdornerWithTemplate(UIElement designerItem, Control template)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            _template = template;
            _template.DataContext = designerItem;
            _visuals = new VisualCollection(this);
            _visuals.Add(_template);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            _template.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }
    }
}
