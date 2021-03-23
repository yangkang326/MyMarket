using System;
using System.Windows;
using MaterialDesignExtensions.Controls;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     EditCargoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditCargoWindow : MaterialWindow
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