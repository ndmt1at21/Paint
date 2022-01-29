using Newtonsoft.Json;
using Paint.Adorner;
using Paint.Commands;
using Paint.Commands.Export;
using Paint.Commands.MoveData;
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
        private IconPath imgPaths;
        private BackupService<ProjectStore> _backupService { get; set; }
        private SaveService<ProjectStore> _saveProjectService { get; set; }
        private LoadService<ProjectStore> _loadProjectService { get; set; }
        private AutoSaveService<ProjectStore> _autoSaveService { get; set; }
        private IPersister<ProjectStore> _persisterProject { get; set; }
        private FontFamily fontStyle;

        private string fontSize;
        private String typeOfColour { get; set; }
        private string isFill { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand LoadProjectCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }
        public ICommand SaveOrSaveAsCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public ICommand CutCommand { get; set; }
        public ICommand PasteCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectAllCommand { get; set; }

        public ObservableCollection<NodeViewModel> Nodes { get; set; }
        public ObservableCollection<NodeViewModel> SelectedItems { get; set; }
        public Stack<List<NodeViewModel>> UndoStack { get; set; }
        public Stack<List<NodeViewModel>> RedoStack { get; set; }
    }

    public partial class MainWindow
    {
        public MainWindow(PluginManager pluginManager)
        {
            InitializeComponent();

            _pluginManager = pluginManager;

            UndoStack = new Stack<List<NodeViewModel>>();
            RedoStack = new Stack<List<NodeViewModel>>();

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

            _persisterProject = new JsonPersister<ProjectStore>();
            _saveProjectService = new SaveService<ProjectStore>(_persisterProject);
            _loadProjectService = new LoadService<ProjectStore>(_persisterProject);

            _autoSaveService = new AutoSaveService<ProjectStore>(_autoSaveConfig, _persisterProject);
            _autoSaveService.GetSavePath = () => _store.CurrentProjectPath;
            _autoSaveService.GetSaveData = () => ProjectStore.CreateFromStore(_store);

            _backupService = new BackupService<ProjectStore>(_backupConfig, _persisterProject);
            _backupService.GetBackupData = () => new ProjectStore();
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

            CopyCommand = new CopyCommand(this);
            CutCommand = new CutCommand(this);
            PasteCommand = new PasteCommand(this);
            DeleteCommand = new DeleteCommand(this);
            SelectAllCommand = new SelectAllCommand(this);
        }

        private void RegisterStoreChanged()
        {
            _store.OnNodesChanged += HandleNodesChanged;
        }

        private void HandleNodesChanged(ObservableCollection<NodeViewModel> nodes)
        {
            if (nodes != null && !isUpdateFromUndo)
            {
                Debug.WriteLine("handle node changesss");
                UndoStack.Push(Utils.Object.DeepClone(nodes.ToList()));
            }

            isUpdateFromUndo = false;
            isUpdateFromRedo = false;
        }
    }

    public partial class MainWindow
    {
        private static bool isUpdateFromUndo { get; set; }
        private static bool isUpdateFromRedo { get; set; }

        public void LoadFrom(string path)
        {
            LoadProjectCommand.Execute(path);
            _autoSaveService.EnableAutoSave();

            RecentFiles.Shared.AddRecent(_store.CurrentProjectPath);
        }

        private void UndoAction()
        {
            if (UndoStack.Count < 2) return;

            isUpdateFromUndo = true;

            var currentItem = UndoStack.Pop();
            var undoItem = UndoStack.Pop();

            Nodes.Clear();
            SelectedItems.Clear();
            undoItem.ForEach(item =>
            {
                item.IsSelected = false;
                Nodes.Add(item);
            });

            RedoStack.Push(currentItem);
        }

        private void RedoAction()
        {
            if (RedoStack.Count < 2) return;

            isUpdateFromRedo = true;

            var currentItem = RedoStack.Pop();
            var redoItem = RedoStack.Pop();

            Nodes.Clear();
            SelectedItems.Clear();
            redoItem.ForEach(item =>
            {
                item.IsSelected = false;
                Nodes.Add(item);
            });

            UndoStack.Push(currentItem);
        }

        // UI Load
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            Nodes = _store.Nodes;
            SelectedItems = new ObservableCollection<NodeViewModel>();
            NodesControl.ItemsSource = Nodes;
            NodesControl.SelectedItems = SelectedItems;

            ExportCommand = new ExportCommand(NodesControl.DesignCanvas);

            try
            {
                InitializeComponent();

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

                //load in ico path
                imgPaths = new IconPath
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
                    sizeIcoPath = "../IconImg/sizeicon.png"
                };
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
            typeOfColour = "rtloutline";

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

            this.RibbonWin.DataContext = imgPaths;
            tempCombobox = new TextSizeDataContext
            {
                size = 10.ToString(),
            };
            this.fontNameCombobox.SelectedItem = System.Drawing.FontFamily.Families[2];

            this.textSizeCombobox.SelectedItem = tempCombobox;
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
            UndoAction();
        }

        private void redoBtnEvenListener(object sender, RoutedEventArgs e)
        {
            RedoAction();
        }

        private void createNewFileBtnEvenListener(object sender, RoutedEventArgs e)
        {
            NewCommand.Execute(null);
        }

        private void openFileBtnEvenListener(object sender, RoutedEventArgs e)
        {
            OpenCommand.Execute(null);
        }

        private void saveAsJPGBtnEvenListener(object sender, RoutedEventArgs e)
        {
            ExportCommand.Execute(null);
        }

        private void saveAsPNGBtnEvenListener(object sender, RoutedEventArgs e)
        {
            ExportCommand.Execute(null);
        }

        private void exitAsPNGBtnEvenListener(object sender, RoutedEventArgs e)
        {
            ExitCommand.Execute(null);
        }

        private void pasteBtnEvenListener(object sender, RoutedEventArgs e)
        {
            PasteCommand.Execute(null);
        }

        private void cutBtnEvenListener(object sender, RoutedEventArgs e)
        {
            CutCommand.Execute(null);
        }

        private void copyBtnEvenListener(object sender, RoutedEventArgs e)
        {
            CopyCommand.Execute(null);
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



        private void pickedTextBtnEvenListener(object sender, RoutedEventArgs e)
        {
            TextNodeViewModel viewModel = new TextNodeViewModel
            {
                Fill = Brushes.Transparent,
            };

            Nodes.Add(viewModel);
            NodesControl.DrawingNode = viewModel;
        }

        private void pickedEarserBtnEvenListener(object sender, RoutedEventArgs e)
        {
            foreach (var item in NodesControl.SelectedItems)
            {
                Nodes.Remove(item);
            }
        }


        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (typeOfColour == "rtlfill")
            {
                rtlfill.Fill = new SolidColorBrush((Color)ClrPcker_Background.SelectedColor);

                foreach (var item in NodesControl.SelectedItems)
                {
                    item.Fill = rtlfill.Fill;
                }
            }
            else if (typeOfColour == "rtloutline")
            {
                rtloutline.Fill = new SolidColorBrush((Color)ClrPcker_Background.SelectedColor);

                foreach (var item in NodesControl.SelectedItems)
                {
                    item.Stroke = rtlfill.Fill;
                }
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
            if (typeOfColour == "rtlfill")
            {
                rtlfill.Fill = rectangle.Fill;

                foreach (var item in NodesControl.SelectedItems)
                {
                    item.Fill = rectangle.Fill;
                }
            }
            else if (typeOfColour == "rtloutline")
            {
                rtloutline.Fill = rectangle.Fill;

                foreach (var item in NodesControl.SelectedItems)
                {
                    item.Stroke = rectangle.Fill;
                }
            }
        }
        private void brushSelected(object sender, RoutedEventArgs e)
        {
            var brushe1 = e.Source as RibbonGalleryItem;
            var brush = brushe1.Content as Rectangle;
            var strokedash = brush.StrokeDashArray;

            foreach (var item in NodesControl.SelectedItems)
            {
                item.StrokeDashArray = strokedash;
            }
        }
        private void chooseShapeBtnClick(object sender, RoutedEventArgs e)
        {
            var a = e.Source as RibbonButton;
            var pluginID = (string)a.Tag;

            IShapeNode shapeNode = _pluginManager.CreateShape(pluginID);

            var shapeNodeVM = new ShapeNodeViewModel
            {
                DefiningShape = shapeNode.CreateGeometry(),
                Fill = Brushes.LightGray
            };

            Nodes.Add(shapeNodeVM);
            NodesControl.DrawingNode = shapeNodeVM;
        }

        private void textSizeChangeEventListenter(object sender, SelectionChangedEventArgs e)
        {
            var textSize = e.Source as ComboBox;
            var data = (TextSizeDataContext)textSize.SelectedItem;

            foreach (var item in NodesControl.SelectedItems)
            {
                if (item is TextNodeViewModel textNodeVM)
                {
                    textNodeVM.FontSize = int.Parse(data.size);
                }
            }
        }
        private void fontChangeEventListenter(object sender, SelectionChangedEventArgs e)
        {
            var font = e.Source as ComboBox;
            var fontFamily = (System.Drawing.FontFamily)font.SelectedItem;

            foreach (var item in NodesControl.SelectedItems)
            {
                if (item is TextNodeViewModel textNodeVM)
                {
                    textNodeVM.FontFamily = new FontFamily(fontFamily.Name);
                }
            }
        }


        private void textStyleClick(object sender, RoutedEventArgs e)
        {
            var textStyle = e.Source as RibbonButton;
            var textStyleTag = (string)textStyle.Tag;

            foreach (var item in NodesControl.SelectedItems)
            {
                if (item is TextNodeViewModel textNodeVM)
                {
                    if (textStyleTag == "Bold")
                    {
                        textNodeVM.FontWeight = FontWeights.Bold;
                    }

                    if (textStyleTag == "Italic")
                    {
                        textNodeVM.FontStyle = FontStyles.Italic;
                    }

                    if (textStyleTag == "Underline")
                    {
                        textNodeVM.TextDecorations = TextDecorations.Underline;
                    }
                }
            }
        }

        private void SelectFillOrOutline(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chooseTypeToColourEvenListener1(object sender, MouseButtonEventArgs e)
        {
            typeOfColour = "rtloutline";
            rtloutline.StrokeThickness = 3;
            rtlfill.StrokeThickness = 0;
        }
        private void chooseTypeToColourEvenListener2(object sender, MouseButtonEventArgs e)
        {
            typeOfColour = "rtlfill";
            rtloutline.StrokeThickness = 0;
            rtlfill.StrokeThickness = 3;
        }

        private void sizeSelected(object sender, RoutedEventArgs e)
        {
            var brushe1 = e.Source as RibbonGalleryItem;
            var brush = brushe1.Content as Rectangle;
            var strokeThick = brush.StrokeThickness;

            foreach (var item in NodesControl.SelectedItems)
            {
                item.StrokeThickness = strokeThick;
            }
        }
    }
}
