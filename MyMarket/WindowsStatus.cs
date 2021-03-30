using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace MyMarket
{
    public static class WindowsStatus
    {
        public static bool MainWindowOpen { get; set; } = false;
        public static bool MenuWindowOpen { get; set; } = false;
        public static bool CargoCheckWindowOpen { get; set; } = false;
        public static bool CargoEditWindowOpen { get; set; } = false;

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        public static Bitmap ConvertToImageSource(string base64String)
        {
            try
            {
                var arr = Convert.FromBase64String(base64String);
                var ms = new MemoryStream(arr);
                var bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                var result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.DecodePixelHeight = 120;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}