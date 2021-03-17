using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.DbOperate;
using MyMarket.Models;

namespace MyMarket.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private int _CartCount;
        private ObservableCollection<CartItem> _CartList;
        private ObservableCollection<string> _GroupNameCollection;
        private ObservableCollection<CargoInfoModel> _ProductsCollection;
        private double _TotalPrice;

        public MainViewModel()
        {
            _CartCount = 0;
            _GroupNameCollection = new ObservableCollection<string>();
            foreach (var GoodsGroup in DbConn.GetCargosGroups())
                GroupNameCollection.Add(GoodsGroup.PDGroup);
            _ProductsCollection = new ObservableCollection<CargoInfoModel>();
            _CartList = new ObservableCollection<CartItem>();
            ProductsCollection = DbConn.GetCargoInfoModels("*");
            AddToCratCommand = new RelayCommand<CargoInfoModel>(e =>
            {
                CartList.Add(new CartItem
                {
                    PDName = e.PDName,
                    PDSN = e.PDCode,
                    UnitPrice = e.PDSellPrice,
                    Count = 1
                });
                TotalPrice = DOAddTotal(CartList);
            });
            PdContChangedCommand = new RelayCommand(() => { TotalPrice = DOAddTotal(CartList); });
            DeleCartItemCommand = new RelayCommand<CartItem>(e =>
            {
                CartList.Remove(e);
                TotalPrice = DOAddTotal(CartList);
            });
            PrintDealDetialCommand = new RelayCommand<DataGrid>(g =>
            {
                var pd = new PrintDialog();
                if (pd.ShowDialog() == true)
                {
                    g.Arrange(new Rect(new Size(g.ActualWidth, g.ActualHeight)));
                    pd.PrintVisual(g, "111");
                }
            });
            SelectGropuChangedCommand = new RelayCommand<object>(o => { ProductsCollection = DbConn.GetCargoInfoModels((string)o); });
        }

        public ObservableCollection<string> GroupNameCollection
        {
            get => _GroupNameCollection;
            set
            {
                _GroupNameCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> SelectGropuChangedCommand { get; set; }
        public RelayCommand<CartItem> DeleCartItemCommand { get; set; }

        public ObservableCollection<CartItem> CartList
        {
            get => _CartList;
            set
            {
                _CartList = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<CargoInfoModel> AddToCratCommand { get; set; }

        public ObservableCollection<CargoInfoModel> ProductsCollection
        {
            get => _ProductsCollection;
            set
            {
                _ProductsCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand PdContChangedCommand { get; set; }

        public double TotalPrice
        {
            get => _TotalPrice;
            set
            {
                _TotalPrice = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<DataGrid> PrintDealDetialCommand { get; set; }

        public int CartCount
        {
            get => _CartCount;
            set
            {
                _CartCount = value;
                OnPropertyChanged();
            }
        }

        private void ChangGoodsGoup(string groupname)
        {
            ProductsCollection = DbConn.GetCargoInfoModels(groupname);
        }

        private double DOAddTotal(ObservableCollection<CartItem> listcartitems)
        {
            double temp = 0;
            foreach (var listcartiteml in listcartitems) temp += listcartiteml.PDTotalPrice;

            CartCount = listcartitems.Count;
            return temp;
        }
    }
}