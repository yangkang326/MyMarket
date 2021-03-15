using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
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
            SaveThisGoodC0mmand = new RelayCommand(async () =>
            {
                var repo = DbConn.fsql.GetRepository<GoodInfoModel>();
                repo.AsTable(oldname => $"{oldname}_{NewDetialMoedl.PDGroup}");
                if (!(repo.Where(o => o.PDCode == NewDetialMoedl.PDCode).Count() > 0))
                    repo.Insert(new GoodInfoModel
                    {
                        IsCommunicationNeeded = NewDetialMoedl.IsCommunicationNeeded,
                        IsVipDiscount = NewDetialMoedl.IsVipDiscount,
                        IsVipPointInc = NewDetialMoedl.IsVipPointInc,
                        IsWeighedNeeded = NewDetialMoedl.IsWeighedNeeded,
                        PDCode = NewDetialMoedl.PDCode,
                        PDCost = NewDetialMoedl.PDCost,
                        PDGroup = NewDetialMoedl.PDGroup,
                        PDName = NewDetialMoedl.PDName,
                        PDProfit = NewDetialMoedl.PDProfit,
                        PDSubName = NewDetialMoedl.PDSubName,
                        PDStock = NewDetialMoedl.PDStock,
                        PDSellPrice = NewDetialMoedl.PDSellPrice,
                        PDSupplier = NewDetialMoedl.PDSupplier,
                        WeighSN = NewDetialMoedl.WeighSN,
                        PicPath = NewDetialMoedl.PicPath
                    });
            });
            AddAnothercommand = new RelayCommand(() =>
            {
                var repo = DbConn.fsql.GetRepository<GoodInfoModel>();
                repo.AsTable(oldname => $"{oldname}_{NewDetialMoedl.PDGroup}");
                if (!(repo.Where(o => o.PDCode == NewDetialMoedl.PDCode).Count() > 0))
                    repo.Insert(new GoodInfoModel
                    {
                        IsCommunicationNeeded = NewDetialMoedl.IsCommunicationNeeded,
                        IsVipDiscount = NewDetialMoedl.IsVipDiscount,
                        IsVipPointInc = NewDetialMoedl.IsVipPointInc,
                        IsWeighedNeeded = NewDetialMoedl.IsWeighedNeeded,
                        PDCode = NewDetialMoedl.PDCode,
                        PDCost = NewDetialMoedl.PDCost,
                        PDGroup = NewDetialMoedl.PDGroup,
                        PDName = NewDetialMoedl.PDName,
                        PDProfit = NewDetialMoedl.PDProfit,
                        PDSubName = NewDetialMoedl.PDSubName,
                        PDStock = NewDetialMoedl.PDStock,
                        PDSellPrice = NewDetialMoedl.PDSellPrice,
                        PDSupplier = NewDetialMoedl.PDSupplier,
                        WeighSN = NewDetialMoedl.WeighSN,
                        PicPath = NewDetialMoedl.PicPath
                    });
                NewDetialMoedl = new GoodInfoModel();
            });
            AddGroupDiaClosedCommand = new RelayCommand<string>(s =>
            {
                if ((s != null) & (s.Length >= 2) && !GroupNameCollection.Contains(s))
                {
                    DbConn.fsql.Insert(new GoodsGroup {PDGroup = s}).ExecuteAffrows();
                    GroupNameCollection = new ObservableCollection<string>();
                    foreach (var GoodsGroup in DbConn.fsql.Select<GoodsGroup>().ToList())
                        GroupNameCollection.Add(GoodsGroup.PDGroup);
                }
                else
                {
                    MessageBox.Show("组名不符合要求或组名已存在");
                }
            });
            SelectPicPath = new RelayCommand<TextBox>(T =>
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|"
                                + "Windows Bitmap(*.bmp)|*.bmp|"
                                + "Windows Icon(*.ico)|*.ico|"
                                + "Graphics Interchange Format (*.gif)|(*.gif)|"
                                + "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|"
                                + "Portable Network Graphics (*.png)|*.png|"
                                + "Tag Image File Format (*.tif)|*.tif;*.tiff";
                var result = dialog.ShowDialog();
                if ((bool) result) T.Text = dialog.FileName;
            });
        }

        public RelayCommand AddAnothercommand { get; set; }


        public RelayCommand<TextBox> SelectPicPath { get; set; }

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