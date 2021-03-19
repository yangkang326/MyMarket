using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.DbOperate;
using MyMarket.Models;

namespace MyMarket.CargosManger.ViewModel
{
    public class CheckViewModel : ObservableObject
    {
        private ObservableCollection<CargoInfoModel> _CargoCollection;

        private ObservableCollection<CargosGroup> _GroupNameCollection;

        public CheckViewModel()
        {
            _CargoCollection = DbConn.GetCargoInfoModels("*");
            _GroupNameCollection = DbConn.GetCargosGroups();
            _GroupNameCollection.Add(new CargosGroup {PDGroup = "*"});
            SelectGropuChangedCommand = new RelayCommand<CargosGroup>(g =>
            {
                CargoCollection = DbConn.GetCargoInfoModels(g.PDGroup);
            });
        }

        public ObservableCollection<CargoInfoModel> CargoCollection
        {
            get => _CargoCollection;
            set
            {
                _CargoCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CargosGroup> GroupNameCollection
        {
            get => _GroupNameCollection;
            set
            {
                _GroupNameCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<CargosGroup> SelectGropuChangedCommand { get; set; }
    }
}