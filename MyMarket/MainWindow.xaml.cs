#region

using System.Windows;

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
    }
}