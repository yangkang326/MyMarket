#region

using System;
using System.Resources;
using System.Windows;
using MaterialDesignExtensions.Controls;

#endregion

namespace MyMarket.MainWin.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MaterialWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closed+=Dispose;
            App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private static MainWindow Instance;
        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }

        public static MainWindow GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MainWindow();
            }
            return Instance;
        }

    }
}