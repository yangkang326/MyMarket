using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using MarketMobileApp.Models;
using MarketMobileApp.Webapi;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Xamarin.Forms;

namespace MarketMobileApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<CargoInfoModel> _CargosCollection;

        private double _CartCost;

        private int _CartCount;
        private ObservableCollection<CartItem> _CartItemsCollection;

        private ObservableCollection<CargosGroup> _GroupsCollection;

        public MainViewModel()
        {
            WebApiOperate.StatiCargoInfoModels = WebApiOperate.GetAllCargoInfoModels();
            WebApiOperate.StatiCargosGroups = WebApiOperate.GetAllGroup();
            _CargosCollection = WebApiOperate.StatiCargoInfoModels;
            _GroupsCollection = WebApiOperate.StatiCargosGroups;
            _CartItemsCollection = new ObservableCollection<CartItem>();

            SelectByGroupName = new RelayCommand<string>(s =>
            {
                var result = WebApiOperate.StatiCargoInfoModels.Where(i => i.PDGroup == s).ToList();
                CargosCollection = new ObservableCollection<CargoInfoModel>(result);
            });

            SelectBystring = new RelayCommand<string>(s =>
            {
                var reslut = WebApiOperate.StatiCargoInfoModels.Where(i => i.PDGroup == s || i.PDCode.Contains(s) || i.PDName.Contains(s) || i.PDSubName.Contains(s)).ToList();
                CargosCollection = new ObservableCollection<CargoInfoModel>(reslut);
            });
            AddToCart = new RelayCommand<CargoInfoModel>(i =>
            {
                double Pdcnt = 1;
                if (i.IsWeighedNeeded)
                {
                    Thread.Sleep(500);
                    Pdcnt = 2;
                }

                CartItemsCollection.Add(new CartItem
                {
                    Count = Pdcnt,
                    PDName = i.PDName,
                    PDSn = i.PDCode,
                    UnitPrice = i.PDSellPrice,
                    PDTotalPrice = Pdcnt * i.PDSellPrice
                });
                CartCost = CartItemsCollection.Sum(a => a.PDTotalPrice);
                CartCount = CartItemsCollection.Count;
            });
            DeleCargoBycode = new RelayCommand<string>(s =>
            {
                WebApiOperate.StatiCargoInfoModels = WebApiOperate.DeleCargo(s);
                CargosCollection = WebApiOperate.StatiCargoInfoModels;
            });
            RefreshCommand = new RelayCommand<RefreshView>(rv =>
            {
                rv.IsRefreshing = true;
                WebApiOperate.StatiCargoInfoModels = WebApiOperate.GetAllCargoInfoModels();
                WebApiOperate.StatiCargosGroups = WebApiOperate.GetAllGroup();
                CargosCollection = WebApiOperate.StatiCargoInfoModels;
                GroupsCollection = WebApiOperate.StatiCargosGroups;
                rv.IsRefreshing = false;
            });
        }

        public ObservableCollection<CartItem> CartItemsCollection
        {
            get => _CartItemsCollection;
            set
            {
                _CartItemsCollection = value;
                OnPropertyChanged();
            }
        }

        public int CartCount
        {
            get => _CartCount;
            set
            {
                _CartCount = value;
                OnPropertyChanged();
            }
        }

        public double CartCost
        {
            get => _CartCost;
            set
            {
                _CartCost = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<RefreshView> RefreshCommand { get; set; }
        public RelayCommand<string> DeleCargoBycode { get; set; }

        public ObservableCollection<CargoInfoModel> CargosCollection
        {
            get => _CargosCollection;
            set
            {
                _CargosCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CargosGroup> GroupsCollection
        {
            get => _GroupsCollection;
            set
            {
                _GroupsCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<string> SelectByGroupName { get; set; }
        public RelayCommand<string> SelectBystring { get; set; }
        public RelayCommand<CargoInfoModel> AddToCart { get; set; }
    }
}