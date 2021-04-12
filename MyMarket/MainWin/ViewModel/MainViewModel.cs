using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            _GroupNameCollection = WebApiOperate.StatiCargosGroups;
            _CargoInfoCollection = WebApiOperate.StatiCargoInfoModels;
            ToEndCommand = new RelayCommand<ScrollViewer>(d =>
            {
                d.ScrollToBottom();
            });
            AddToCratCommand = new RelayCommand<CargoInfoModel>(e =>
            {
                AddToCart(e);
            });
            PdContChangedCommand = new RelayCommand<CartItem>(async s =>
            {
                var Temp = await WebApiOperate.CheckStock(s.PDSn);
                s.Count = s.Count > Temp ? Temp : s.Count;
                CurrentTotalPrice = DoAddTotal(CurentCargosCollection);
            });
            DeleCartItemCommand = new RelayCommand<CartItem>(e =>
            {
                CurentCargosCollection.Remove(e);
                CurrentTotalPrice = DoAddTotal(CurentCargosCollection);
            });
            PayDetialCommand = new RelayCommand(() =>
            {
                var Win = PayWindow.GetInstace();
                Win.ShowDialog();
            });
            SelectGropuChangedCommand = new RelayCommand<CargosGroup>(o =>
            {
                var result = WebApiOperate.StatiCargoInfoModels.Where(i => i.PDGroup == o.PDGroup).ToList();
                CargoInfoCollection = new ObservableCollection<CargoInfoModel>(result);
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
                var Menuwin = MenuView.GetInstance();
                Menuwin.Show();
                Menuwin.Closed += Update;
            });
            GetHoldCartByIndexCommand = new RelayCommand<int>(i =>
            {
                if (CurentCargosCollection.Count > 0)
                {
                    var Result = MessageBox.Show("当前购物车未结算，是否保存", "挂单处理", MessageBoxButton.YesNoCancel);
                    if (Result == MessageBoxResult.Yes)
                    {
                        HoldCartsCollection.Add(CurentCargosCollection);
                        CurentCargosCollection = HoldCartsCollection[i - 1];
                        HoldCartsCollection.RemoveAt(i - 1);
                        HoldCartsIndexCollection.RemoveAt(HoldCartsIndexCollection.Count - 1);
                        HoldCount = HoldCartsCollection.Count;
                        HoldCartsIndexCollection.Add(HoldCount);
                    }
                    else if (Result == MessageBoxResult.No)
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
                var reslut = WebApiOperate.StatiCargoInfoModels.Where(i => i.PDGroup == value || i.PDCode.Contains(value) || i.PDName.Contains(value) || i.PDSubName.Contains(value)).ToList();
                CargoInfoCollection = new ObservableCollection<CargoInfoModel>(reslut);
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

        private void Update(object sender, EventArgs e)
        {
            WebApiOperate.StatiCargoInfoModels = WebApiOperate.StatiCargoInfoModels;
            WebApiOperate.StatiCargosGroups = WebApiOperate.StatiCargosGroups;
            CargoInfoCollection = WebApiOperate.StatiCargoInfoModels;
            GroupNameCollection = WebApiOperate.StatiCargosGroups;
        }

        private async void AddToCart(CargoInfoModel c)
        {
            var Tenmcont = 1.0;
            if (c.IsWeighedNeeded)
            {
                await Task.Delay(1000);
                Tenmcont = 20;
            }

            CurentCargosCollection.Add(new CartItem
            {
                PDName = c.PDName,
                PDSn = c.PDCode,
                UnitPrice = c.PDSellPrice,
                Count = Tenmcont
            });
            CurrentTotalPrice = DoAddTotal(CurentCargosCollection);
        }

        private void Decode(object recipient, string message)
        {
            if (!WindowsStatus.CargoEditWindowOpen) InputSearchString = message;
            var result = WebApiOperate.StatiCargoInfoModels.Where(i => i.PDCode == message).ToList();
            CargoInfoCollection = new ObservableCollection<CargoInfoModel>(result);
            if (CargoInfoCollection.Count == 1)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CurentCargosCollection.Add(new CartItem
                    {
                        PDName = CargoInfoCollection[0].PDName,
                        PDSn = CargoInfoCollection[0].PDCode,
                        UnitPrice = CargoInfoCollection[0].PDSellPrice,
                        Count = CargoInfoCollection[0].IsWeighedNeeded ? GetWeight(CargoInfoCollection[0].PDSellPrice) : 1
                    });
                });
                CurrentTotalPrice = DoAddTotal(CurentCargosCollection);
                InputSearchString = "";
            }
        }

        private double GetWeight(double unitPrice)
        {
            double Result = 0;
            Result = 30;
            return Result;
        }


        private double DoAddTotal(ObservableCollection<CartItem> listcartitems)
        {
            double Temp = 0;
            foreach (var Listcartiteml in listcartitems) Temp += Listcartiteml.PDTotalPrice;
            CartCount = listcartitems.Count;
            return Temp;
        }
    }
}