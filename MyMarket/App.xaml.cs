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
            WebApiOperate.StatiCargoInfoModels = WebApiOperate.GetAllCargoInfoModels().Result;
            WebApiOperate.StatiCargosGroups = WebApiOperate.GetAllGroup().Result;
            WebApiOperate.StatiCargosUnits = WebApiOperate.GetAllUnit().Result;
            var Win = MainWin.View.MainWindow.GetInstance();
            WindowsStatus.MainWindowOpen = true;
            Win.Show();
        }
    }
}