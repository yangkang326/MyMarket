using System.Collections.ObjectModel;
using System.Linq;
using MarketMobileApp.Models;
using MarketMobileApp.Webapi;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace MarketMobileApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<CargoInfoModel> _CargosCollection;

        private ObservableCollection<CargosGroup> _GroupsCollection;

        public MainViewModel()
        {
            WebApiOperate.StatiCargoInfoModels = WebApiOperate.GetAllCargoInfoModels();
            WebApiOperate.StatiCargosGroups = WebApiOperate.GetAllGroup();
            _CargosCollection = WebApiOperate.StatiCargoInfoModels;
            _GroupsCollection = WebApiOperate.StatiCargosGroups;
            SelectByGroupName = new RelayCommand<string>(s =>
            {
                var result = WebApiOperate.StatiCargoInfoModels.Where(i => i.PDGroup == s).ToList();
                CargosCollection = new ObservableCollection<CargoInfoModel>(result);
            });
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
        }

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
    }
}