using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.CargosManger.View;
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
            EditRelayCommand = new RelayCommand<CargoInfoModel>(c =>
            {
                var win = AddNewCargo.GetInstance();
                (win.DataContext as CargoEditViewModel).NewDetialMoedl = c;
                (win.DataContext as CargoEditViewModel).EditModel = "修改";
                win.Show();
                win.Closed += UpdateList;
            });
            AddNewRelayCommand = new RelayCommand(() =>
            {
                var win = AddNewCargo.GetInstance();
                (win.DataContext as CargoEditViewModel).EditModel = "保存";
                win.Show();
                win.Closed += UpdateList;
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

        public RelayCommand AddNewRelayCommand { get; set; }
        public RelayCommand<CargoInfoModel> EditRelayCommand { get; set; }
        public RelayCommand<CargoInfoModel> DeleRelayCommand { get; set; }
        public RelayCommand<CargosGroup> SelectGropuChangedCommand { get; set; }

        private void UpdateList(object sender, EventArgs e)
        {
            CargoCollection = DbConn.GetCargoInfoModels("*");
        }
    }
}