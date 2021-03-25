using System;
using System.Windows;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     CheckModel.xaml 的交互逻辑
    /// </summary>
    public partial class CargosCheckModel : Window
    {
        private static CargosCheckModel Instance;

        private CargosCheckModel()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }

        public static CargosCheckModel GetInstance()
        {
            if (Instance == null) Instance = new CargosCheckModel();
            return Instance;
        }
    }
}