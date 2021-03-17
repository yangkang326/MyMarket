using System;
using System.Windows;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     AddNewCargo.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewCargo : Window
    {
        private AddNewCargo()
        {
            InitializeComponent();
            this.Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }

        static AddNewCargo Instance = null;

        public static AddNewCargo GetInstance()
        {
            if (Instance == null)
            {
                Instance = new AddNewCargo();
            }

            return Instance;
        }
    }
}