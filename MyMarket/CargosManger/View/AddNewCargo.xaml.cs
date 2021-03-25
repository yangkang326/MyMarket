﻿using System;
using System.Windows;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MyMarket.CargosManger.View
{
    /// <summary>
    ///     AddNewCargo.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewCargo : Window
    {
        private static AddNewCargo Instance;

        private AddNewCargo()
        {
            InitializeComponent();
            Closed += Dispose;
        }

        private void Dispose(object sender, EventArgs e)
        {
            Instance = null;
            WindowsStatus.CargoEditWindowOpen = false;
            WeakReferenceMessenger.Default.UnregisterAll(DataContext);
        }

        public static AddNewCargo GetInstance()
        {
            if (Instance == null) Instance = new AddNewCargo();
            return Instance;
        }
    }
}