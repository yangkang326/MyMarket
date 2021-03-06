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
    public class CargoEditViewModel : ObservableObject, IDisposable
    {
        private string _EditModel;
        private ObservableCollection<CargosGroup> _GroupNameCollection;
        private string _InputUnit;
        private CargoInfoModel _NewDetialMoedl;
        private string _NewGroupNameInput;
        private string _PicPath;
        private string _SelectedGroupName;
        private string _SelectedUnits;
        private ObservableCollection<CargoUnit> _UnitsCollection;

        public CargoEditViewModel()
        {
            _SelectedUnits = "";
            _UnitsCollection = WebApiOperate.StatiCargosUnits;
            _SelectedGroupName = "";
            _PicPath = "";
            _EditModel = "添加";
            WeakReferenceMessenger.Default.Register<string, string>(this, "DataCom", Decode);
            WindowsStatus.CargoEditWindowOpen = true;
            _GroupNameCollection = WebApiOperate.StatiCargosGroups;
            PDCreatePdCodeCommand = new RelayCommand(() =>
            {
                var S = DateTime.Now.ToString("yyyyMMddHHmmss") + WebApiOperate.StatiCargoInfoModels.Count.ToString("D5");
                NewDetialMoedl.PDCode = S;
            });
            PDSaveThisGoodC0Mmand = new RelayCommand(() =>
            {
                WebApiOperate.InserOrUpdateCargo(NewDetialMoedl);
            });
            AddGroupNameCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(NewGroupNameInput)) WebApiOperate.StatiCargosGroups = WebApiOperate.InserOrUpdateGroup(NewGroupNameInput).Result;
                GroupNameCollection = WebApiOperate.StatiCargosGroups;
                NewGroupNameInput = "";
            });
            AddUnitCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(InputUnit)) WebApiOperate.StatiCargosUnits = WebApiOperate.InserOrUpdateunit(InputUnit).Result;
                UnitsCollection = WebApiOperate.StatiCargosUnits;
                InputUnit = "";
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
            GroupSelectedCommand = new RelayCommand<CargosGroup>(g =>
            {
                if (g != null)
                {
                    SelectedGroupName = g.PDGroup;
                }
                else
                {
                    SelectedGroupName = "";
                }

                if (NewDetialMoedl == null)
                {
                    NewDetialMoedl = new CargoInfoModel();
                }

                NewDetialMoedl.PDGroup = g.PDGroup;
            });
            UnitSelectedComamnd = new RelayCommand<CargoUnit>(u =>
            {
                if (u != null)
                {
                    SelectedUnits = u.Unit;
                }
                else
                {
                    SelectedUnits = "";
                }

                if (NewDetialMoedl == null)
                {
                    NewDetialMoedl = new CargoInfoModel();
                }

                NewDetialMoedl.PDUnit = u.Unit;
            });
        }

        public string InputUnit
        {
            get => _InputUnit;
            set
            {
                _InputUnit = value;
                OnPropertyChanged();
            }
        }

        public string SelectedUnits
        {
            get => _SelectedUnits;
            set
            {
                _SelectedUnits = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CargoUnit> UnitsCollection
        {
            get => _UnitsCollection;
            set
            {
                _UnitsCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddUnitCommand { get; set; }
        public RelayCommand<CargoUnit> UnitSelectedComamnd { get; set; }

        public string SelectedGroupName
        {
            get => _SelectedGroupName;
            set
            {
                _SelectedGroupName = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<CargosGroup> GroupSelectedCommand { get; set; }

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

        public ObservableCollection<CargosGroup> GroupNameCollection
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        private void Decode(object recipient, string message)
        {
            NewDetialMoedl = new CargoInfoModel();
            if (WindowsStatus.CargoEditWindowOpen) NewDetialMoedl.PDCode = message;
        }
    }
}