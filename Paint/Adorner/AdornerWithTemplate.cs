using System;
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
        private VisualCollection visuals;
        private UserControl chrome;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        public AdornerWithTemplate(UIElement designerItem, UserControl template)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            chrome = template;
            chrome.DataContext = designerItem;
            visuals = new VisualCollection(this);
            visuals.Add(chrome);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }
    }
}
