using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MyLib;
using MyMarket.AllMenu.View;
using MyMarket.Pay.View;

namespace MyMarket.MainWin.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<CargoInfoModel> _CargoInfoCollection = new();
        private int _CartCargosCount;
        private ObservableCollection<CartItem> _CurentCargosCollection = new();
        private double _CurrentCurrentTotalPrice;
        private ObservableCollection<CargosGroup> _GroupNameCollection;
        private ObservableCollection<ObservableCollection<CartItem>> _HoldCartsCollection = new();
        private ObservableCollection<int> _HoldCartsIndexCollection = new();
        private int _HoldCount;
        private string _InputSearchString = "";

        public MainViewModel()
        {
            WeakReferenceMessenger.Default.Register<string, string>(this, "DataCom", Decode);
            _GroupNameCollection = WebApiOperate.GetAllGroup();
            _CargoInfoCollection = WebApiOperate.GetCargoInfoModels("");
            ToEndCommand = new RelayCommand<ScrollViewer>(d => { d.ScrollToBottom(); });
            AddToCratCommand = new RelayCommand<CargoInfoModel>(e => { ADDToCart(e); });
            PdContChangedCommand = new RelayCommand<CartItem>(s =>
            {
                var temp = WebApiOperate.CheckStock(s.PDSN);
                s.Count = s.Count > temp ? temp : s.Count;
                CurrentTotalPrice = DOAddTotal(CurentCargosCollection);
            });
            DeleCartItemCommand = new RelayCommand<CartItem>(e =>
            {
                CurentCargosCollection.Remove(e);
                CurrentTotalPrice = DOAddTotal(CurentCargosCollection);
            });
            PayDetialCommand = new RelayCommand(() =>
            {
                var win = PayWindow.GetInstace();
                win.ShowDialog();
            });
            SelectGropuChangedCommand = new RelayCommand<CargosGroup>(o =>
            {
                CargoInfoCollection = WebApiOperate.GetCargoInfoModelsByGroupName(o.PDGroup);
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
            OpenMenuViewCommand = new RelayCommand(() =>
            {
                var menuwin = MenuView.GetInstance();
                menuwin.Show();
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
        }

        public RelayCommand<ScrollViewer> ToEndCommand { get; set; }

        public ObservableCollection<CargosGroup> GroupNameCollection
        {
            get => _GroupNameCollection;
            set
            {
                _GroupNameCollection = value;
                OnPropertyChanged();
            }
        }

        public string InputSearchString
        {
            get => _InputSearchString;
            set
            {
                _InputSearchString = value;
                CargoInfoCollection = WebApiOperate.GetCargoInfoModels(value);
                OnPropertyChanged();
            }
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

        public RelayCommand<CargosGroup> SelectGropuChangedCommand { get; set; }
        public RelayCommand<CartItem> DeleCartItemCommand { get; set; }
        public RelayCommand OpenMenuViewCommand { get; set; }

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

        public RelayCommand<CartItem> PdContChangedCommand { get; set; }

        public double CurrentTotalPrice
        {
            get => _CurrentCurrentTotalPrice;
            set
            {
                _CurrentCurrentTotalPrice = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand PayDetialCommand { get; set; }

        public int CartCount
        {
            get => _CartCargosCount;
            set
            {
                _CartCargosCount = value;
                OnPropertyChanged();
            }
        }

        private async void ADDToCart(CargoInfoModel c)
        {
            var tenmcont = 1.0;
            if (c.IsWeighedNeeded)
            {
                await Task.Delay(1000);
                tenmcont = 20;
            }

            CurentCargosCollection.Add(new CartItem
            {
                PDName = c.PDName,
                PDSN = c.PDCode,
                UnitPrice = c.PDSellPrice,
                Count = tenmcont
            });
            CurrentTotalPrice = DOAddTotal(CurentCargosCollection);
        }

        private void Decode(object recipient, string message)
        {
            if (!WindowsStatus.CargoEditWindowOpen) InputSearchString = message;
            CargoInfoCollection = WebApiOperate.GetCargoInfoModels(message);
            if (CargoInfoCollection.Count == 1)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CurentCargosCollection.Add(new CartItem
                    {
                        PDName = CargoInfoCollection[0].PDName,
                        PDSN = CargoInfoCollection[0].PDCode,
                        UnitPrice = CargoInfoCollection[0].PDSellPrice,
                        Count = CargoInfoCollection[0].IsWeighedNeeded
                            ? GetWeight(CargoInfoCollection[0].PDSellPrice)
                            : 1
                    });
                });
                CurrentTotalPrice = DOAddTotal(CurentCargosCollection);
                InputSearchString = "";
            }
        }

        private double GetWeight(double UnitPrice)
        {
            double result = 0;
            result = 30;
            return result;
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