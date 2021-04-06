using System;
using System.Windows;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     EditCargoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditCargoWindow : Window
    {
        private static EditCargoWindow _Instance;

        private EditCargoWindow()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            _Instance = null;
        }

        public static EditCargoWindow GetInstance()
        {
            if (_Instance == null) _Instance = new EditCargoWindow();
            return _Instance;
        }
    }
}