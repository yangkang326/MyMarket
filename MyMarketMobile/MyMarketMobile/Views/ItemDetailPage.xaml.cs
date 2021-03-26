using MyMarketMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyMarketMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}