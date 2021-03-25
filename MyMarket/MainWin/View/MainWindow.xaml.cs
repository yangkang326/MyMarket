#region

using System;
using System.ComponentModel;
using System.Windows;

#endregion

namespace MyMarket.MainWin.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();
            WindowsStatus.MainWindowOpen = true;
            Closing += Closeing;
            Closed += Dispose;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void Closeing(object sender, CancelEventArgs e)
        {
            WindowsStatus.MainWindowOpen = false;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }

        public static MainWindow GetInstance()
        {
            if (Instance == null) Instance = new MainWindow();
            return Instance;
        }
    }
}