using System;
using System.Windows;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     CargosInHousingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CargosInHousingWindow : Window
    {
        private static CargosInHousingWindow _Instance;

        private CargosInHousingWindow()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            _Instance = null;
        }

        public static CargosInHousingWindow GetInstance()
        {
            if (_Instance == null) _Instance = new CargosInHousingWindow();
            return _Instance;
        }
    }
}