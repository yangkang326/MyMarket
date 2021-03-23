#region

using System.Windows;
using MyMarket.MainWin.View;
using MyMarket.Scanner;

#endregion

namespace MyMarket
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Scan.GetSerialPort("COM5");
            Scan.OpenPort();
            var win = MyMarket.MainWin.View.MainWindow.GetInstance();
            WindowsStatus.MainWindowOpen = true;
            win.Show();
        }
    }
}