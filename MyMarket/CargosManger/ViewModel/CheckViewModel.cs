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
        private CargoInfoModel _SelectInfoModel;

        public CheckViewModel()
        {
            _CargoCollection = DbConn.GetCargoInfoModels("*");
            _GroupNameCollection = DbConn.GetCargosGroups();
            _GroupNameCollection.Add(new CargosGroup {PDGroup = "*"});
            SelectGropuChangedCommand = new RelayCommand<CargosGroup>(g =>
            {
                CargoCollection = DbConn.GetCargoInfoModels(g.PDGroup);
            });
            EditRelayCommand = new RelayCommand(() =>
            {
                var win = AddNewCargo.GetInstance();
                (win.DataContext as CargoEditViewModel).NewDetialMoedl = SelectInfoModel;
                (win.DataContext as CargoEditViewModel).EditModel = "修改";
                win.Show();
                win.Closed += UpdateList;
            });
            AddNewRelayCommand = new RelayCommand(() =>
            {
                SelectInfoModel = null;
                var win = AddNewCargo.GetInstance();
                (win.DataContext as CargoEditViewModel).NewDetialMoedl = SelectInfoModel;
                (win.DataContext as CargoEditViewModel).EditModel = "保存";
                win.Show();
                win.Closed += UpdateList;
            });
            DeleRelayCommand = new RelayCommand(() =>
            {
                DbConn.fsql.Delete<CargoInfoModel>().Where(i => i.PDCode == SelectInfoModel.PDCode).ExecuteAffrows();
                CargoCollection = DbConn.GetCargoInfoModels("");
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
        public RelayCommand EditRelayCommand { get; set; }
        public RelayCommand DeleRelayCommand { get; set; }
        public RelayCommand<CargosGroup> SelectGropuChangedCommand { get; set; }

        public CargoInfoModel SelectInfoModel
        {
            get => _SelectInfoModel;
            set
            {
                _SelectInfoModel = value;
                OnPropertyChanged();
            }
        }

        private void UpdateList(object sender, EventArgs e)
        {
            CargoCollection = DbConn.GetCargoInfoModels("*");
        }
    }
}