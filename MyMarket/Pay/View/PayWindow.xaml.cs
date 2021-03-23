using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignExtensions.Controls;

namespace MyMarket.Pay.View
{
    /// <summary>
    /// PayWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PayWindow : MaterialWindow
    {
        private PayWindow()
        {
            InitializeComponent();
            this.Closed += Dispose;
        }

        private static PayWindow Instance;

        public static PayWindow GetInstace()
        {
            if (Instance == null)
            {
                Instance = new PayWindow();
            }
            return Instance;
        }
        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }
    }
}
