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
                var Arr = Convert.FromBase64String(base64String);
                var Ms = new MemoryStream(Arr);
                var Bmp = new Bitmap(Ms);
                Ms.Close();
                return Bmp;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (var Stream = new MemoryStream())
            {
                bitmap.Save(Stream, ImageFormat.Png);
                Stream.Position = 0;
                var Result = new BitmapImage();
                Result.BeginInit();
                Result.CacheOption = BitmapCacheOption.OnLoad;
                Result.StreamSource = Stream;
                Result.DecodePixelHeight = 120;
                Result.EndInit();
                Result.Freeze();
                return Result;
            }
        }
    }
}