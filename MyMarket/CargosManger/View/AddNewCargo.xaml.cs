using System;
using System.Windows;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     AddNewCargo.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewCargo : Window
    {
        private static AddNewCargo _Instance;

        private AddNewCargo()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            _Instance = null;
            WindowsStatus.CargoEditWindowOpen = false;
            WeakReferenceMessenger.Default.UnregisterAll(DataContext);
        }

        public static AddNewCargo GetInstance()
        {
            if (_Instance == null) _Instance = new AddNewCargo();
            return _Instance;
        }
    }
}