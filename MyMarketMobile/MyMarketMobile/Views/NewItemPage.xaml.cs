using MyMarketMobile.Models;
using MyMarketMobile.ViewModels;
using Xamarin.Forms;

namespace MyMarketMobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }

        public Item Item { get; set; }
    }
}