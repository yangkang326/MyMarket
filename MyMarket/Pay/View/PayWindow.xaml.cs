using System;
using System.Windows;

namespace MyMarket.Pay.View
{
    /// <summary>
    ///     PayWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PayWindow : Window
    {
        private static PayWindow _Instance;

        private PayWindow()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        public static PayWindow GetInstace()
        {
            if (_Instance == null) _Instance = new PayWindow();
            return _Instance;
        }

        private void Dispose(object sender, EventArgs e)
        {
            _Instance = null;
        }
    }
}