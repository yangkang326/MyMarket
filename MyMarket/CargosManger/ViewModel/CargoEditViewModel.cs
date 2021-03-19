using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Win32;
using MyMarket.DbOperate;
using MyMarket.Models;

namespace MyMarket.CargosManger.ViewModel
{
    public class CargoEditViewModel : ObservableObject
    {
        public static bool IsActivated;
        private ObservableCollection<string> _GroupNameCollection;
        private CargoInfoModel _NewDetialMoedl;

        public CargoEditViewModel()
        {
            WeakReferenceMessenger.Default.Register<string, string>(this, "DataCom", Decode);
            IsActivated = true;
            _GroupNameCollection = new ObservableCollection<string>();
            foreach (var GoodsGroup in DbConn.fsql.Select<CargosGroup>().ToList())
                GroupNameCollection.Add(GoodsGroup.PDGroup);
            _NewDetialMoedl = new CargoInfoModel();
            ChangeProfitCommand = new RelayCommand(() =>
            {
                if (NewDetialMoedl.PDCost > 0)
                    NewDetialMoedl.PDProfit = NewDetialMoedl.PDSellPrice / NewDetialMoedl.PDCost - 1;
                else
                    NewDetialMoedl.PDProfit = 0;
            });
            CreatePDCodeCommand = new RelayCommand(() => { NewDetialMoedl.PDCode = "我去你大爷的"; });
            SaveThisGoodC0mmand = new RelayCommand(() => { DbConn.InsertCargoInfoModels(NewDetialMoedl); });
            AddAnothercommand = new RelayCommand(() =>
            {
                DbConn.InsertCargoInfoModels(NewDetialMoedl);
                NewDetialMoedl = new CargoInfoModel();
            });
            AddGroupDiaClosedCommand = new RelayCommand<string>(s =>
            {
                if (s != null && s.Length >= 2 && !GroupNameCollection.Contains(s))
                {
                    GroupNameCollection = new ObservableCollection<string>();
                    foreach (var GoodsGroup in DbConn.InsertCargosGroups(s))
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
            ClosedCommand = new RelayCommand(() => { IsActivated = false; });
        }

        public RelayCommand AddAnothercommand { get; set; }
        public RelayCommand ClosedCommand { get; set; }
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

        public CargoInfoModel NewDetialMoedl
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

        private void Decode(object recipient, string message)
        {
            if (IsActivated) NewDetialMoedl.PDCode = message;
        }
    }
}