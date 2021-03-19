#region

using System.Windows;
using MyMarket.CargosManger.View;

#endregion

namespace MyMarket
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyHidDevice_DataReceived(byte[] data)
        {
            var a = data;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var addwin = AddNewCargo.GetInstance();
            addwin.Show();
        }
    }
}