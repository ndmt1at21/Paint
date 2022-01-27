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

        public MainWindow(PluginManager pluginManager)
        {
            try
            {
                InitializeComponent();
                Nodes = new ObservableCollection<NodeViewModel>();

                _pluginManager = pluginManager;

                NodesControl.ItemsSource = Nodes;

                for (int i = 0; i < 10; i++)
                {
                    Nodes.Add(new ShapeNodeViewModel
                    {
                        DefiningShape = new RectangleGeometry(new Rect(0, 0, 1, 1)),
                        Width = 100,
                        Height = 100,
                        Top = i * 50,
                        Left = i * 50,
                        Fill = new SolidColorBrush(Color.FromArgb(255, 255, (byte)(i * 10), (byte)(i * 10))),
                        ZIndex = 10 - i
                    }); ;
                }
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
                };
                DataContext = imgPaths;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BindingList<PropertyInfo> tempItemSource = new BindingList<PropertyInfo>();
            PropertyInfo temp;
            for (var i = 0; i < 10; i++)
            {
                temp = typeof(Brushes).GetProperties()[i*5];
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

        }
        private void pickedBlackColorBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }

        private void pickedWhiteColorBtnEvenListener(object sender, RoutedEventArgs e)
        {

        }
        private void colorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Brush selectedColor = (Brush)(e.AddedItems[0] as PropertyInfo).GetValue(null, null);

            rtlfill.Fill = selectedColor;

        }

    }
}
