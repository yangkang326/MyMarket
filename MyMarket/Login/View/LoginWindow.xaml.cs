using System.Windows;
using System.Windows.Controls;

namespace MyMarket.Login.View
{
    /// <summary>
    ///     LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var Closebtn = GetTemplateChild("CloseButton") as Button;
        }
    }
}