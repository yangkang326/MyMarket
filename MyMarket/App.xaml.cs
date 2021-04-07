#region

using System.Windows;
using MyLib;
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
            WindowsStatus.StatiCargoInfoModels = WebApiOperate.GetCargoInfoModels();
            WindowsStatus.StatiCargosGroups = WebApiOperate.GetAllGroup();
            var Win = MainWin.View.MainWindow.GetInstance();
            WindowsStatus.MainWindowOpen = true;
            Win.Show();
        }
    }
}