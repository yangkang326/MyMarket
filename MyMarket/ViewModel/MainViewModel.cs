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
        private ObservableCollection<ObservableCollection<CartItem>> _HoldCartsCollection;
        private ObservableCollection<int> _HoldCartsIndexCollection;
        private int _HoldCount;
        private ObservableCollection<CargoInfoModel> _ProductsCollection;
        private double _TotalPrice;
        private string _SelectedString;

        public string SelectedString
        {
            get => _SelectedString;
            set
            {
                _SelectedString = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<string> SelectCargoByStringCommand { get; set; }
        public MainViewModel()
        {
            _CartCount = 0;
            _HoldCount = 0;
            _HoldCartsIndexCollection = new ObservableCollection<int>();
            _GroupNameCollection = new ObservableCollection<string>();
            foreach (var GoodsGroup in DbConn.GetCargosGroups())
                GroupNameCollection.Add(GoodsGroup.PDGroup);
            _ProductsCollection = new ObservableCollection<CargoInfoModel>();
            _HoldCartsCollection = new ObservableCollection<ObservableCollection<CartItem>>();
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
            SelectGropuChangedCommand = new RelayCommand<object>(o =>
            {
                ProductsCollection = DbConn.GetCargoInfoModels((string) o);
            });
            HoldThisCartCommand = new RelayCommand(() =>
            {
                if (CartList.Count > 0)
                {
                    HoldCartsCollection.Add(CartList);
                    CartList = new ObservableCollection<CartItem>();
                    HoldCount = HoldCartsCollection.Count;
                    HoldCartsIndexCollection.Add(HoldCount);
                }
            });
            GetHoldCartByIndexCommand = new RelayCommand<int>(i =>
            {
                if (CartList.Count > 0)
                {
                    var result = MessageBox.Show("当前购物车未结算，是否保存", "挂单处理", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        HoldCartsCollection.Add(CartList);
                        CartList = HoldCartsCollection[i - 1];
                        HoldCartsCollection.RemoveAt(i - 1);
                        HoldCartsIndexCollection.RemoveAt(HoldCartsIndexCollection.Count - 1);
                        HoldCount = HoldCartsCollection.Count;
                        HoldCartsIndexCollection.Add(HoldCount);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        CartList = HoldCartsCollection[i - 1];
                        HoldCartsCollection.RemoveAt(i - 1);
                        HoldCartsIndexCollection.RemoveAt(HoldCartsIndexCollection.Count - 1);
                        HoldCount = HoldCartsCollection.Count;
                    }
                }
                else
                {
                    CartList = HoldCartsCollection[i - 1];
                    HoldCartsCollection.RemoveAt(i - 1);
                    HoldCartsIndexCollection.RemoveAt(HoldCartsIndexCollection.Count - 1);
                    HoldCount = HoldCartsCollection.Count;
                }
            });
            SelectCargoByStringCommand = new RelayCommand<string>((s) =>
            {
                ProductsCollection = DbConn.GetCargoInfoModelsByString(s);
            });
        }

        public int HoldCount
        {
            get => _HoldCount;
            set
            {
                _HoldCount = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand HoldThisCartCommand { get; set; }
        public RelayCommand<int> GetHoldCartByIndexCommand { get; set; }

        public ObservableCollection<int> HoldCartsIndexCollection
        {
            get => _HoldCartsIndexCollection;
            set
            {
                _HoldCartsIndexCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ObservableCollection<CartItem>> HoldCartsCollection
        {
            get => _HoldCartsCollection;
            set
            {
                _HoldCartsCollection = value;
                OnPropertyChanged();
            }
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


        private double DOAddTotal(ObservableCollection<CartItem> listcartitems)
        {
            double temp = 0;
            foreach (var listcartiteml in listcartitems) temp += listcartiteml.PDTotalPrice;
            CartCount = listcartitems.Count;
            return temp;
        }
    }
}