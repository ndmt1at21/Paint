using Microsoft.Win32;
using Paint.CustomControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paint.Commands.Export
{
    public class ExportCommand : CommandBase
    {
        private DesignCanvas _canvas;

        public ExportCommand(DesignCanvas canvas)
        {
            _canvas = canvas;
        }

        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpeg; *jpg|PNG Image|*.png";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                VisualBrush visualBrush = new VisualBrush(_canvas);
                drawingContext.DrawRectangle(visualBrush, null, VisualTreeHelper.GetDescendantBounds(_canvas));
                drawingContext.Close();

                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                    (int)_canvas.ActualWidth, (int)_canvas.ActualHeight,
                    96d, 96d, PixelFormats.Pbgra32
                );
                renderBitmap.Render(drawingVisual);

                BitmapEncoder encoder = null;

                string fileExtension = System.IO.Path.GetExtension(saveFileDialog.FileName);

                switch (fileExtension.ToLower())
                {
                    case ".bmp":
                        encoder = new BmpBitmapEncoder();
                        break;
                    case ".jpg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".jpeg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".png":
                        encoder = new PngBitmapEncoder();
                        break;
                }

                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (Stream stm = File.Create(saveFileDialog.FileName))
                {
                    encoder.Save(stm);
                }
            }
        }
    }
}
