using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.AllMenu.Model;
using MyMarket.CargosManger.View;

namespace MyMarket.AllMenu.ViewModel
{
    public class MenuViewModel : ObservableObject
    {
        private ObservableCollection<MenuModel> _MenuNameCollection;

        public MenuViewModel()
        {
            _MenuNameCollection = new ObservableCollection<MenuModel>();
            MenuNameCollection.Add(new MenuModel
            {
                Name = "交接班结算",
                Icon = "\xe659"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "会员管理",
                Icon = "\xe64a"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "销售明细",
                Icon = "\xe66f"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "退货",
                Icon = "\xe665"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "进货管理",
                Icon = "\xe654"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "调拨申请",
                Icon = "\xe667"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "盘点",
                Icon = "\xe656"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "商品信息",
                Icon = "\xe65f"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "规则设置",
                Icon = "\xe652"
            });
            MenuNameCollection.Add(new MenuModel
            {
                Name = "报损",
                Icon = "\xe647"
            });
            MemuButtonClickCommand = new RelayCommand<MenuModel>(m =>
            {
                switch (m.Name)
                {
                    case "商品信息":
                        var win = CargosCheckModel.GetInstance();
                        win.Show();
                        break;
                }
            });
        }

        public ObservableCollection<MenuModel> MenuNameCollection
        {
            get => _MenuNameCollection;
            set
            {
                _MenuNameCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<MenuModel> MemuButtonClickCommand { get; set; }
    }
}