using System;
using System.Windows;

namespace MyMarket.AllMenu.View
{
    /// <summary>
    ///     MenuView.xaml 的交互逻辑
    /// </summary>
    public partial class MenuView : Window
    {
        private static MenuView Instance;

        private MenuView()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
        }

        public static MenuView GetInstance()
        {
            if (Instance == null) Instance = new MenuView();
            return Instance;
        }
    }
}