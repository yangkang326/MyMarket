using System;
using System.Windows;

namespace MyMarket.Pay.View
{
    /// <summary>
    ///     PayWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PayWindow : Window
    {
        private static PayWindow Instance;

        private PayWindow()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        public static PayWindow GetInstace()
        {
            if (Instance == null) Instance = new PayWindow();
            return Instance;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }
    }
}