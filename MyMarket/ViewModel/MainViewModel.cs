using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        private ObservableCollection<CargoInfoModel> _CargoInfoCollection = new();
        private int _CartCargosCount;
        private ObservableCollection<CartItem> _CurentCargosCollection = new();
        private double _CurrentCurrentTotalPrice;
        private ObservableCollection<string> _GroupNameCollection = new();
        private ObservableCollection<ObservableCollection<CartItem>> _HoldCartsCollection = new();
        private ObservableCollection<int> _HoldCartsIndexCollection = new();
        private int _HoldCount;
        private string _InputSearchString = "";

        public MainViewModel()
        {
            _CartCargosCount = 0;
            _HoldCount = 0;
            foreach (var GoodsGroup in DbConn.GetCargosGroups())
                GroupNameCollection.Add(GoodsGroup.PDGroup);
            CargoInfoCollection = DbConn.GetCargoInfoModels("*");
            AddToCratCommand = new RelayCommand<CargoInfoModel>(e =>
            {
                CurentCargosCollection.Add(new CartItem
                {
                    PDName = e.PDName,
                    PDSN = e.PDCode,
                    UnitPrice = e.PDSellPrice,
                    Count = e.IsWeighedNeeded ? GetWeight(e.PDSellPrice) : 1
                });
                CurrentTotalPrice = DOAddTotal(CurentCargosCollection);
            });
            PdContChangedCommand = new RelayCommand<object>(s =>
            {
                var tempobj = s as CartItem;
                var temp = DbConn.fsql.Select<CargoInfoModel>().Where(i => i.PDCode == tempobj.PDSN).First().PDStock;
                tempobj.Count = tempobj.Count > temp ? temp : tempobj.Count;
                CurrentTotalPrice = DOAddTotal(CurentCargosCollection);
            });
            DeleCartItemCommand = new RelayCommand<CartItem>(e =>
            {
                CurentCargosCollection.Remove(e);
                CurrentTotalPrice = DOAddTotal(CurentCargosCollection);
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
                CargoInfoCollection = DbConn.GetCargoInfoModels((string) o);
            });
            HoldThisCartCommand = new RelayCommand(() =>
            {
                if (CurentCargosCollection.Count > 0)
                {
                    HoldCartsCollection.Add(CurentCargosCollection);
                    CurentCargosCollection = new ObservableCollection<CartItem>();
                    HoldCount = HoldCartsCollection.Count;
                    HoldCartsIndexCollection.Add(HoldCount);
                }
            });
            GetHoldCartByIndexCommand = new RelayCommand<int>(i =>
            {
                if (CurentCargosCollection.Count > 0)
                {
                    var result = MessageBox.Show("当前购物车未结算，是否保存", "挂单处理", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        HoldCartsCollection.Add(CurentCargosCollection);
                        CurentCargosCollection = HoldCartsCollection[i - 1];
                        HoldCartsCollection.RemoveAt(i - 1);
                        HoldCartsIndexCollection.RemoveAt(HoldCartsIndexCollection.Count - 1);
                        HoldCount = HoldCartsCollection.Count;
                        HoldCartsIndexCollection.Add(HoldCount);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        CurentCargosCollection = HoldCartsCollection[i - 1];
                        HoldCartsCollection.RemoveAt(i - 1);
                        HoldCartsIndexCollection.RemoveAt(HoldCartsIndexCollection.Count - 1);
                        HoldCount = HoldCartsCollection.Count;
                    }
                }
                else
                {
                    CurentCargosCollection = HoldCartsCollection[i - 1];
                    HoldCartsCollection.RemoveAt(i - 1);
                    HoldCartsIndexCollection.RemoveAt(HoldCartsIndexCollection.Count - 1);
                    HoldCount = HoldCartsCollection.Count;
                }
            });
            SelectCargoByStringCommand = new RelayCommand<string>(s =>
            {
                CargoInfoCollection = DbConn.GetCargoInfoModelsByString(s);
            });
        }

        public string SelectedString
        {
            get => _InputSearchString;
            set
            {
                _InputSearchString = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<string> SelectCargoByStringCommand { get; set; }

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

        public ObservableCollection<CartItem> CurentCargosCollection
        {
            get => _CurentCargosCollection;
            set
            {
                _CurentCargosCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<CargoInfoModel> AddToCratCommand { get; set; }

        public ObservableCollection<CargoInfoModel> CargoInfoCollection
        {
            get => _CargoInfoCollection;
            set
            {
                _CargoInfoCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> PdContChangedCommand { get; set; }

        public double CurrentTotalPrice
        {
            get => _CurrentCurrentTotalPrice;
            set
            {
                _CurrentCurrentTotalPrice = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<DataGrid> PrintDealDetialCommand { get; set; }

        public int CartCount
        {
            get => _CartCargosCount;
            set
            {
                _CartCargosCount = value;
                OnPropertyChanged();
            }
        }

        private double GetWeight(double UnitPrice)
        {
            double result = 0;
            Task.Run(async () => { });
            return result;
            ;
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