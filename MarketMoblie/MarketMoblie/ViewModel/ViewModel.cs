using System.Collections.ObjectModel;
using MarketMoblie.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MarketMoblie.ViewModel
{
    public class ViewModel : ObservableObject
    {
        private ObservableCollection<CargoInfoModel> _CargoInfoModels;

        public ViewModel()
        {
            _CargoInfoModels = new ObservableCollection<CargoInfoModel>
            {
                new CargoInfoModel
                {
                    PDName = "123", PDCode = "12", PDSellPrice = 10, PDUnit = "个",
                    PicPath = "pack://application:,,,/Image/西瓜.png"
                },
                new CargoInfoModel
                {
                    PDName = "234", PDCode = "4", PDSellPrice = 20, PDUnit = "个",
                    PicPath = "pack://application:,,,/Image/香蕉.png"
                },
                new CargoInfoModel
                {
                    PDName = "345", PDCode = "3", PDSellPrice = 30, PDUnit = "个",
                    PicPath = "pack://application:,,,/Image/香梨.png"
                },
                new CargoInfoModel
                {
                    PDName = "456", PDCode = "2", PDSellPrice = 40, PDUnit = "个",
                    PicPath = "pack://application:,,,/Image/椰子.png"
                },
                new CargoInfoModel
                {
                    PDName = "678", PDCode = "1", PDSellPrice = 50, PDUnit = "个",
                    PicPath = "pack://application:,,,/Image/樱桃.png"
                }
            };
        }

        public ObservableCollection<CargoInfoModel> CargoInfoModels
        {
            get => _CargoInfoModels;
            set
            {
                _CargoInfoModels = value;
                OnPropertyChanged();
            }
        }
    }
}