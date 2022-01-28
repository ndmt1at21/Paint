using Paint.Adorner;
using Paint.IconImg;
using Paint.Lib;
using Paint.ViewModels;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint.Views
{
    public partial class MainWindow : RibbonWindow
    {
        private PluginManager _pluginManager { get; set; }

        public ObservableCollection<NodeViewModel> Nodes { get; set; }

        public List<List<NodeViewModel>> NodeList { get; set; }
        public List<NodeViewModel> NodeListPresent { get; set; }

        public void UndoAction()
        {
            if (NodeList != null)
            {
                for (int i = 1; i < NodeList.Count(); i++)
                {
                    if (NodeList[i] == NodeListPresent)
                    {
                        NodeListPresent = NodeList[i - 1];
                    }
                }
            }

        }

        public void RedoAction()
        {
            if (NodeList != null)
            {
                for (int i = 0; i < NodeList.Count() - 1; i++)
                {
                    if (NodeList[i] == NodeListPresent)
                    {
                        NodeListPresent = NodeList[i + 1];
                    }
                }
            }
        }
        public void Copy()
        {
            foreach (var item in Nodes)
                if (item.IsSelected)
                {
                    Clipboard.SetDataObject(item.Clone());
                }
        }
        public void Cut()
        {
            foreach (var item in Nodes)
                if (item.IsSelected)
                {
                    Clipboard.SetDataObject(item.Clone());
                    Nodes.Remove(item);
                }
        }
        public void Paste()
        {
            List<NodeViewModel> list = new List<NodeViewModel>();
            list = Clipboard.GetDataObject() as List<NodeViewModel>;
            if (list == null)
                return;
            foreach (var item in list)
                Nodes.Add(item);
        }

        public MainWindow(PluginManager pluginManager)
        {
            try
            {
                InitializeComponent();
                Nodes = new ObservableCollection<NodeViewModel>();

                _pluginManager = pluginManager;

                NodesControl.ItemsSource = Nodes;


                //load in plugin
                string[] pluginIDs = _pluginManager.GetPluginIDs();
                BindingList<PluginItemsForDataContext> shapeItemSource = new BindingList<PluginItemsForDataContext>();

                for (int i = 0; i < pluginIDs.Length; i++)
                {
                    PluginItemsForDataContext pluginItemsForDataContext = new PluginItemsForDataContext
                    {
                        PluginID = pluginIDs[i]
                    };
                    shapeItemSource.Add(pluginItemsForDataContext);


                }
                shapeList.ItemsSource = shapeItemSource;


                //test
                for (int i = 0; i < 10; i++)
                {
                    Nodes.Add(new LineNodeViewModel
                    {
                        Top = 10,
                        Left = 10,
                        Width = 100,
                        Height = 100,
                        X1 = 10,
                        X2 = 100,
                        Y1 = 10,
                        Y2 = 100,
                        StrokeThickness = 2,
                        Stroke = new SolidColorBrush(Colors.Black),
                    });
                }

                //load in ico path
                var imgPaths = new IconPath
                {
                    saveIcoPath = "../IconImg/saveicon.png",

                    undoIcoPath = "../IconImg/undoicon.png",
                    redoIcoPath = "../IconImg/redoicon.png",
                    cutIcoPath = "../IconImg/cuticon.png",
                    copyIcoPath = "../IconImg/copyicon.png",
                    pasteIcoPath = "../IconImg/pasteicon.png",
                    textIcoPath = "../IconImg/texticon.png",
                    cropIcoPath = "../IconImg/resizeicon.png",
                    selectIcoPath = "../IconImg/selecticon.png",
                    rotateIcoPath = "../IconImg/rotateicon.png",
                    brushIcoPath = "../IconImg/penicon.png",
                    shapesIcoPath = "../IconImg/shapeicon.png",
                    horizonIcoPath = "../IconImg/horizonicon.png",
                    verticalIcoPath = "../IconImg/verticalicon.png",
                    rotateleftIcoPath = "../IconImg/rotatelefticon.png",
                    rotaterightIcoPath = "../IconImg/rotaterighticon.png",
                    color1IcoPath = "../IconImg/blackicon.png",
                    color2IcoPath = "../IconImg/whiteicon.png",
                    colorpickerIcoPath = "../IconImg/colorpickicon.png",
                    fillIcoPath = "../IconImg/fillicon.png",
                    outlineIcoPath = "../IconImg/outlineicon.png",
                    zoomIcoPath = "../IconImg/zoomicon.png",
                    eraseIcoPath = "../IconImg/erasericon.png",
                    newFileIcoPath = "../IconImg/newfileicon.png",
                    openIcoPath = "../IconImg/openfileicon.png",
                    exitIcoPath = "../IconImg/exiticon.png",

                };
                DataContext = imgPaths;

                //init set colorpick color
                ClrPcker_Background.SelectedColor = Color.FromRgb(255, 255, 255);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BindingList<ColorPickerForDataContext> tempItemSource = new BindingList<ColorPickerForDataContext>();
            ColorPickerForDataContext temp;

            temp = new ColorPickerForDataContext
            {
                Name = "Black"
            };
            tempItemSource.Add(temp);
            temp = new ColorPickerForDataContext
            {
                Name = "White"
            };
            tempItemSource.Add(temp);
            for (var i = 0; i < 25; i++)
            {
                temp = new ColorPickerForDataContext
                {
                    Name = typeof(Brushes).GetProperties()[i * 5].Name
                };

                tempItemSource.Add(temp);
            }
            this.colorList.ItemsSource = tempItemSource;



        }



        private void saveBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }



        private void undoBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void redoBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void createNewFileBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void openFileBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void saveAsJPGBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void saveAsPNGBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void exitAsPNGBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void pasteBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void cutBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void copyBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void selectBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void resizeBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void flipHorizontalBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void filpVerticalBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void rotateLeftBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void rotateRightBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void pickedPenBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void pickedBucketFillBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void pickedTextBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void pickedEarserBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void zoomBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            rtlfill.Fill = new SolidColorBrush((Color)ClrPcker_Background.SelectedColor);
        }

        private void colorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Brush selectedColor = (Brush)(e.AddedItems[0] as PropertyInfo).GetValue(null, null);

            rtlfill.Fill = selectedColor;

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = e.Source as Rectangle;
            rtlfill.Fill = rectangle.Fill;
        }

        private void chooseShapeBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
