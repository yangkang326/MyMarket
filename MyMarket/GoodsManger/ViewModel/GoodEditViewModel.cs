using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.GoodsManger.Model;
using MyMarket.Models;

namespace MyMarket.GoodsManger.ViewModel
{
    public class GoodEditViewModel : ObservableObject
    {
        private GoodInfoModel _NewDetialMoedl;

        public GoodEditViewModel()
        {
            _GrouppCollection = new ObservableCollection<string>();
            foreach (var GoodsGroup in DbOperate.DbConn.fsql.Select<GoodsGroup>().ToList())
            {
                GrouppCollection.Add(GoodsGroup.PDGroup);
            }
            _NewDetialMoedl = new GoodInfoModel();
            ChangeProfitCommand = new RelayCommand(() =>
            {
                if (NewDetialMoedl.PDCost > 0)
                    NewDetialMoedl.PDProfit = NewDetialMoedl.PDSellPrice / NewDetialMoedl.PDCost - 1;
                else
                    NewDetialMoedl.PDProfit = 0;
            });
            CreatePDCodeCommand = new RelayCommand(() => { NewDetialMoedl.PDCode = "我去你大爷的"; });
            SaveThisGoodC0mmand = new RelayCommand(async () =>
            {

            });
        }

        private ObservableCollection<string> _GrouppCollection;

        public ObservableCollection<string> GrouppCollection
        {
            get => _GrouppCollection;
            set
            {
                _GrouppCollection = value;
                OnPropertyChanged();
            }
        }
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