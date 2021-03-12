using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.Models;

namespace MyMarket.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private int _CartCount;
        private ObservableCollection<CartItem> _CartList;
        private ObservableCollection<GoodsIcon> _ProductsCollection;
        private double _TotalPrice;

        public MainViewModel()
        {
            _CartCount = 0;
            _ProductsCollection = new ObservableCollection<GoodsIcon>();
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "红苹果",
                PDUnitPrice = 6.5,
                PDUnit = "500g",
                PDSN = "01",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/红苹果.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "青苹果",
                PDUnitPrice = 3.0,
                PDUnit = "500g",
                PDSN = "02",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/青苹果.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "香蕉",
                PDUnitPrice = 4.5,
                PDUnit = "500g",
                PDSN = "03",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/香蕉.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "西瓜",
                PDUnitPrice = 7.8,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/西瓜.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "草莓",
                PDUnitPrice = 15,
                PDUnit = "500g",
                PDSN = "01",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/草莓.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "葡萄",
                PDUnitPrice = 9.9,
                PDUnit = "500g",
                PDSN = "02",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/葡萄.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "黑提",
                PDUnitPrice = 19.9,
                PDUnit = "500g",
                PDSN = "03",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/黑提.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "猕猴桃",
                PDUnitPrice = 6.9,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/猕猴桃.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "油桃",
                PDUnitPrice = 9.9,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/油桃.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "茶Π茉莉花味",
                PDUnitPrice = 9.9,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/香梨.png"
            });
            _CartList = new ObservableCollection<CartItem>();
            AddToCratCommand = new RelayCommand<GoodsIcon>(e =>
            {
                CartList.Add(new CartItem
                {
                    PDName = e.PDName,
                    PDSN = e.PDSN,
                    UnitPrice = e.PDUnitPrice,
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
        }

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

        public RelayCommand<GoodsIcon> AddToCratCommand { get; set; }

        public ObservableCollection<GoodsIcon> ProductsCollection
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
            foreach (var listcartiteml in listcartitems)
            {
                temp += listcartiteml.PDTotalPrice;
                CartCount = listcartitems.Count;
            }

            return temp;
        }
    }
}