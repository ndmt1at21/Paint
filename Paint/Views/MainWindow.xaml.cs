﻿using Paint.Adorner;
using Paint.Commands;
using Paint.IconImg;
using Paint.Lib;
using Paint.Models;
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
        public static int ProjectNumber { get; set; } = 0;
        public string CurrentProjectName { get; set; }

        private PluginManager _pluginManager { get; set; }
        private Store _store { get; set; }

        private BackupService<Store> _backupService { get; set; }
        private SaveService<Store> _saveProjectService { get; set; }
        private LoadService<Store> _loadProjectService { get; set; }
        private AutoSaveService<Store> _autoSaveService { get; set; }
        private IPersister<Store> _persisterProject { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand LoadProjectCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }
        public ICommand SaveOrSaveAsCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public ObservableCollection<NodeViewModel> Nodes { get; set; }
        public Stack<ObservableCollection<NodeViewModel>> UndoStack { get; set; }
        public Stack<ObservableCollection<NodeViewModel>> RedoStack { get; set; }

   
    }

    public partial class MainWindow
    {
        public MainWindow(PluginManager pluginManager)
        {
            InitializeComponent();

            _pluginManager = pluginManager;

            DataContext = this;
            NodesControl.ItemsSource = Nodes;

            InitializeStore();
            InitializeServices();
            InitializeCommands();
        }

        private void InitializeStore()
        {
            _store = new Store();
            RegisterStoreChanged();

            _store.CurrentProjectPath = $"pait{ProjectNumber}";
        }

        public void InitializeServices()
        {
            var _autoSaveConfig = new AutoSaveConfig
            {
                AutoSave = true,
                AutoSaveInterval = 30
            };

            var _backupConfig = new BackupConfig
            {
                Directory = Environment.GetEnvironmentVariable("BackupFilesPath"),
                BackupInterval = 10,
                Extension = "paitup",
            };

            _persisterProject = new JsonPersister<Store>();

            _saveProjectService = new SaveService<Store>(_persisterProject);
            _loadProjectService = new LoadService<Store>(_persisterProject);

            _autoSaveService = new AutoSaveService<Store>(_autoSaveConfig, _persisterProject);
            _autoSaveService.GetSavePath = () => _store.CurrentProjectPath;
            _autoSaveService.GetSaveData = () => _store;

            _backupService = new BackupService<Store>(_backupConfig, _persisterProject);
            _backupService.GetBackupData = () => new Store();
            _backupService.GetBackupPath = () => _store.CurrentProjectPath;
            _backupService.StartBackup();
        }

        public void InitializeCommands()
        {
            NewCommand = new NewCommand(_pluginManager);
            OpenCommand = new OpenCommand(_pluginManager);
            LoadProjectCommand = new LoadProjectCommand(_store, _loadProjectService);
            SaveOrSaveAsCommand = new SaveOrSaveAsCommand(_store, _saveProjectService);
            SaveAsCommand = new SaveAsCommand(_store, _saveProjectService);
            ExitCommand = new ExitCommand(_store, this);
        }

        private void RegisterStoreChanged()
        {

        }
    }

    public partial class MainWindow
    {
        public List<List<NodeViewModel>> NodeList { get; set; }
        public List<NodeViewModel> NodeListPresent { get; set; }

        public void LoadFrom(string path)
        {
            LoadProjectCommand.Execute(path);
            _autoSaveService.EnableAutoSave();

            RecentFiles.Shared.AddRecent(_store.CurrentProjectPath);
        }

        private void UndoAction()
        {
            if (UndoStack.Count == 0) return;

            Nodes = UndoStack.Pop();
            RedoStack.Push(new ObservableCollection<NodeViewModel>(Nodes));
        }

        private void RedoAction()
        {
            if (RedoStack.Count == 0) return;

            Nodes = RedoStack.Pop();
            UndoStack.Push(new ObservableCollection<NodeViewModel>(Nodes));
        }

        private void Copy()
        {
            foreach (var item in Nodes)
            {
                if (item.IsSelected)
                {
                    Clipboard.SetDataObject(item.Clone());
                }
            }
        }

        private void Cut()
        {
            foreach (var item in Nodes)
            {
                if (item.IsSelected)
                {
                    Clipboard.SetDataObject(item.Clone());
                    Nodes.Remove(item);
                }
            }
        }

        private void Paste()
        {
            List<NodeViewModel> list = new List<NodeViewModel>();
            list = Clipboard.GetDataObject() as List<NodeViewModel>;

            if (list == null)
                return;

            foreach (var item in list)
            {
                Nodes.Add(item);
            }
        }

        // UI Load
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InitializeComponent();
                Nodes = new ObservableCollection<NodeViewModel>();

                //load in plugin
                string[] pluginIDs = _pluginManager.GetPluginIDs();
                BindingList<PluginItemsForDataContext> shapeItemSource = new BindingList<PluginItemsForDataContext>();

                for (int i = 0; i < pluginIDs.Length; i++)
                {
                    PluginItemsForDataContext pluginItemsForDataContext = new PluginItemsForDataContext
                    {
                        PluginID = pluginIDs[i],
                        PluginIconPath = "../IconImg/" + pluginIDs[i] + ".png"
                    };
                    shapeItemSource.Add(pluginItemsForDataContext);
                    

                }
                shapeList.ItemsSource = shapeItemSource;


                //test
                Nodes.Add(new ShapeNodeViewModel
                {
                    Top = 100,
                    Left = 100,
                    Width = 100,
                    Height = 100,
                    Fill = Brushes.Red,
                    DefiningShape = new RectangleGeometry(new Rect(0, 0, 1, 1))
                });

                Nodes.Add(new ImageNodeViewModel
                {
                    Top = 0,
                    Left = 0,
                    Height = 100,
                    Width = 100,
                  //  ImageSource = new BitmapImage(new Uri("C:\\Users\\ndmt1at21\\Desktop\\escape.png"))
                });

                Nodes.Add(new TextNodeViewModel
                {
                    Top = 0,
                    Left = 0,
                    Height = 100,
                    Width = 100,
                    Content = "dfjghjfhfhjfg"
                });

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
                    brushesIcoPath = "../IconImg/brushesicon.png",
                    italicStyleIcoPath = "../IconImg/italicicon.png",
                    boldStyleIcoPath = "../IconImg/boldicon.png",
                    underlineStyleIcoPath = "../IconImg/underlineicon.png",
                };
                DataContext = imgPaths;

                //init set colorpick color
                ClrPcker_Background.SelectedColor = Color.FromRgb(255, 255, 255);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            BindingList<ColorPickerForDataContext> tempItemSource = new BindingList<ColorPickerForDataContext>();
            ColorPickerForDataContext temp;
            BindingList<TextSizeDataContext> tempItemSourceCombobox = new BindingList<TextSizeDataContext>();
            TextSizeDataContext tempCombobox;
       
            
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

                tempCombobox = new TextSizeDataContext
                {
                    size = (i + 1).ToString()
                };
                tempItemSourceCombobox.Add(tempCombobox);

            }
            for (var i = 0; i < 50; i++)
            {
               

                tempCombobox = new TextSizeDataContext
                {
                    size = (i + 1).ToString()
                };
                tempItemSourceCombobox.Add(tempCombobox);

            }
            this.colorList.ItemsSource = tempItemSource;

            this.textSizeCombobox.ItemsSource = tempItemSourceCombobox;

            var fontFamliles = new System.Drawing.Text.InstalledFontCollection();
            this.fontNameCombobox.ItemsSource = fontFamliles.Families;
            
        }
    }

    // Event Click
    public partial class MainWindow
    {
        private void saveBtnEvenListener(object sender, RoutedEventArgs e)
        {
            SaveOrSaveAsCommand.Execute(null);
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

            foreach (var item in NodesControl.SelectedItems)
            {
                item.Fill = rtlfill.Fill;
            }
        }

        private void colorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Brush selectedColor = (Brush)(e.AddedItems[0] as PropertyInfo).GetValue(null, null);
            rtlfill.Fill = selectedColor;

            foreach (var item in NodesControl.SelectedItems)
            {
                item.Fill = selectedColor;
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = e.Source as Rectangle;
            rtlfill.Fill = rectangle.Fill;

            foreach (var item in NodesControl.SelectedItems)
            {
                item.Fill = rectangle.Fill;
            }
        }
        private void brushSelected(object sender, RoutedEventArgs e)
        {
            var brushe1 = e.Source as RibbonGalleryItem;
            var brush = brushe1.Content as Rectangle;
            var stoke = brush.Stroke;
            var strokedash = brush.StrokeDashArray;
        }
        private void chooseShapeBtnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(sender);
            var a = e.Source as RibbonButton;
            Debug.WriteLine(a);
            var pluginID = a.Tag;
        }
        private void textSizeChangeEventListenter(object sender, SelectionChangedEventArgs e)
        {
            var textSize = e.Source as ComboBox;
            var textSizeString = textSize.SelectedItem;
        }
        private void fontChangeEventListenter(object sender, SelectionChangedEventArgs e)
        {
            var font = e.Source as ComboBox;
            var fontString = font.SelectedItem;
        }


        private void textStyleClick(object sender, RoutedEventArgs e)
        {
            var textStyle = e.Source as RibbonButton;
            var textStyleTag = textStyle.Tag;
        }
    }
}
