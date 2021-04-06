using System;
using System.Windows;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     CheckModel.xaml 的交互逻辑
    /// </summary>
    public partial class CargosCheckModel : Window
    {
        private static CargosCheckModel _Instance;

        private CargosCheckModel()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            _Instance = null;
        }

        public static CargosCheckModel GetInstance()
        {
            if (_Instance == null) _Instance = new CargosCheckModel();
            return _Instance;
        }
    }
}