using System;
using System.Windows;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     CheckModel.xaml 的交互逻辑
    /// </summary>
    public partial class CheckModel : Window
    {
        private static CheckModel Instance;

        private CheckModel()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }

        public static CheckModel GetInstance()
        {
            if (Instance == null) ;
            {
                Instance = new CheckModel();
            }
            return Instance;
        }
    }
}