using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Win32;
using MyLib;

namespace MyMarket.CargosManger.ViewModel
{
    public class CargoEditViewModel : ObservableObject
    {
        private string _EditModel;
        private ObservableCollection<string> _GroupNameCollection;
        private CargoInfoModel _NewDetialMoedl;
        private string _NewGroupNameInput;

        private string _PicPath;

        public CargoEditViewModel()
        {
            _PicPath = "";
            _EditModel = "添加";
            WeakReferenceMessenger.Default.Register<string, string>(this, "DataCom", Decode);
            WindowsStatus.CargoEditWindowOpen = true;
            var AllGroup = WindowsStatus.StatiCargosGroups;
            _GroupNameCollection = new ObservableCollection<string>();
            foreach (var CargosGroup in WebApiOperate.GetAllGroup()) _GroupNameCollection.Add(CargosGroup.PDGroup);
            _NewDetialMoedl = new CargoInfoModel();
            PDCreatePdCodeCommand = new RelayCommand(() =>
            {
                var S = DateTime.Now.ToString("yyyyMMddHHmmss") + WindowsStatus.StatiCargoInfoModels.Count.ToString("D5");
                NewDetialMoedl.PDCode = S;
            });
            PDSaveThisGoodC0Mmand = new RelayCommand(() =>
            {
                WebApiOperate.InserOrUpdateCargo(NewDetialMoedl);
            });
            AddGroupNameCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(NewGroupNameInput)) WebApiOperate.InserOrUpdateGroup(NewGroupNameInput);

                GroupNameCollection = new ObservableCollection<string>();
                foreach (var CargosGroup in WebApiOperate.GetAllGroup()) GroupNameCollection.Add(CargosGroup.PDGroup);
                NewGroupNameInput = "";
            });
            SelectPicPath = new RelayCommand<TextBox>(T =>
            {
                var Dialog = new OpenFileDialog();
                Dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" + "Windows Bitmap(*.bmp)|*.bmp|" + "Windows Icon(*.ico)|*.ico|" + "Graphics Interchange Format (*.gif)|(*.gif)|" + "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" + "Portable Network Graphics (*.png)|*.png|" + "Tag Image File Format (*.tif)|*.tif;*.tiff";
                var Result = Dialog.ShowDialog();
                if ((bool) Result)
                {
                    PicPath = Dialog.FileName;
                    NewDetialMoedl.PicPath = Dialog.SafeFileName;
                    WebApiOperate.UpLoadPic(PicPath);
                }
            });
            ClosedCommand = new RelayCommand(() =>
            {
                WindowsStatus.CargoEditWindowOpen = false;
            });
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

        public string PicPath
        {
            get => _PicPath;
            set
            {
                _PicPath = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand PDCreatePdCodeCommand { get; set; }
        public RelayCommand PDSaveThisGoodC0Mmand { get; set; }

        private void Decode(object recipient, string message)
        {
            NewDetialMoedl = new CargoInfoModel();
            if (WindowsStatus.CargoEditWindowOpen) NewDetialMoedl.PDCode = message;
        }
    }
}