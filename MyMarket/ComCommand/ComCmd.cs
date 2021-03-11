using System.Windows;
using Microsoft.Toolkit.Mvvm.Input;

namespace MyMarket.ComCommand
{
    public class ComCmd
    {
        public  RelayCommand<Window> CloseWinCommand { get; set; } = new RelayCommand<Window>(w =>
        {
            w.Close();
        });
        public  RelayCommand<Window> RestoreWindCommand { get; set; } = new RelayCommand<Window>((w) =>
        {
            w.WindowState = w.WindowState==WindowState.Maximized?WindowState.Maximized:WindowState.Normal;
        });
        public  RelayCommand<Window> MinWindCommand { get; set; } = new RelayCommand<Window>((w) =>
        {
            w.WindowState = WindowState.Minimized;
        });
    }
}