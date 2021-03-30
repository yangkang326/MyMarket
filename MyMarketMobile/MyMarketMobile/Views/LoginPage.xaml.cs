using MyMarketMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMarketMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}