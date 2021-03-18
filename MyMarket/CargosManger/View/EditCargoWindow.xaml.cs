using System;
using System.Windows;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     EditCargoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditCargoWindow : Window
    {
        private static EditCargoWindow Instance;

        private EditCargoWindow()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }

        public static EditCargoWindow GetInstance()
        {
            if (Instance == null) Instance = new EditCargoWindow();
            return Instance;
        }
    }
}