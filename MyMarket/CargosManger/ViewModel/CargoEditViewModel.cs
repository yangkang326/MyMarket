using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Win32;
using MyLib;
using MyMarket.DbOperate;

namespace MyMarket.CargosManger.ViewModel
{
    public class CargoEditViewModel : ObservableObject
    {
        private string _EditModel;
        private ObservableCollection<string> _GroupNameCollection;
        private CargoInfoModel _NewDetialMoedl;
        private string _NewGroupNameInput;

        public CargoEditViewModel()
        {
            _EditModel = "添加";
            WeakReferenceMessenger.Default.Register<string, string>(this, "DataCom", Decode);
            WindowsStatus.CargoEditWindowOpen = true;
            _GroupNameCollection = new ObservableCollection<string>();
            foreach (var GoodsGroup in DbConn.fsql.Select<CargosGroup>().ToList())
                GroupNameCollection.Add(GoodsGroup.PDGroup);
            _NewDetialMoedl = new CargoInfoModel();
            CreatePDCodeCommand = new RelayCommand(() =>
            {
                NewDetialMoedl = new CargoInfoModel();
                var s = DateTime.Now.ToString("yyyyMMddHHmmss") +
                        DbConn.fsql.Select<CargoInfoModel>().ToList().Count.ToString("D5");
                NewDetialMoedl.PDCode = s;
            });
            SaveThisGoodC0mmand = new RelayCommand(() => { DbConn.InsertCargoInfoModels(NewDetialMoedl); });
            AddGroupNameCommand = new RelayCommand(() =>
            {
                if (NewGroupNameInput != null && NewGroupNameInput.Length >= 2 &&
                    !GroupNameCollection.Contains(NewGroupNameInput))
                {
                    GroupNameCollection = new ObservableCollection<string>();
                    foreach (var GoodsGroup in DbConn.InsertCargosGroups(NewGroupNameInput))
                        GroupNameCollection.Add(GoodsGroup.PDGroup);
                }
                else
                {
                    MessageBox.Show("组名不符合要求或组名已存在");
                }

                NewGroupNameInput = "";
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
                if ((bool) result)
                    //T.Text = dialog.FileName;
                    NewDetialMoedl.PicPath = dialog.FileName;
            });
            ClosedCommand = new RelayCommand(() => { WindowsStatus.CargoEditWindowOpen = false; });
        }

        public string NewGroupNameInput
        {
            get => _NewGroupNameInput;
            set
            {
                _NewGroupNameInput = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ClosedCommand { get; set; }
        public RelayCommand<TextBox> SelectPicPath { get; set; }

        public string EditModel
        {
            get => _EditModel;
            set
            {
                _EditModel = value;
                OnPropertyChanged();
            }
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

        public RelayCommand AddGroupNameCommand { get; set; }

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
        public RelayCommand SaveThisGoodC0mmand { get; set; }

        private void Decode(object recipient, string message)
        {
            NewDetialMoedl = new CargoInfoModel();
            if (WindowsStatus.CargoEditWindowOpen) NewDetialMoedl.PDCode = message;
        }
    }
}