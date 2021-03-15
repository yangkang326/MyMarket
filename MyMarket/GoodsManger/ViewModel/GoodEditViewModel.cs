using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.GoodsManger.DbOperate;
using MyMarket.GoodsManger.Model;
using MyMarket.Models;

namespace MyMarket.GoodsManger.ViewModel
{
    public class GoodEditViewModel : ObservableObject
    {
        private ObservableCollection<string> _GroupNameCollection;
        private GoodInfoModel _NewDetialMoedl;

        public GoodEditViewModel()
        {
            _GroupNameCollection = new ObservableCollection<string>();
            foreach (var GoodsGroup in DbConn.fsql.Select<GoodsGroup>().ToList())
                GroupNameCollection.Add(GoodsGroup.PDGroup);
            _NewDetialMoedl = new GoodInfoModel();
            ChangeProfitCommand = new RelayCommand(() =>
            {
                if (NewDetialMoedl.PDCost > 0)
                    NewDetialMoedl.PDProfit = NewDetialMoedl.PDSellPrice / NewDetialMoedl.PDCost - 1;
                else
                    NewDetialMoedl.PDProfit = 0;
            });
            CreatePDCodeCommand = new RelayCommand(() => { NewDetialMoedl.PDCode = "我去你大爷的"; });
            SaveThisGoodC0mmand = new RelayCommand(async () => { });
            AddGroupDiaClosedCommand = new RelayCommand<string>(s =>
            {
                if ((s != null) & (s.Length > 2) && !GroupNameCollection.Contains(s))
                    GroupNameCollection.Add(s);
                else
                    MessageBox.Show("组名不符合要求或组名已存在");
            });
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

        public RelayCommand<string> AddGroupDiaClosedCommand { get; set; }

        public GoodInfoModel NewDetialMoedl
        {
            get => _NewDetialMoedl;
            set
            {
                _NewDetialMoedl = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CreatePDCodeCommand { get; set; }
        public RelayCommand ChangeProfitCommand { get; set; }
        public RelayCommand SaveThisGoodC0mmand { get; set; }
    }
}