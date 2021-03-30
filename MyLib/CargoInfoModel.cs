using FreeSql.DataAnnotations;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MyLib
{
    public class CargoInfoModel : ObservableObject
    {
        private double _PDCost;
        private double _PDProfit;
        private double _PDSellPrice;

        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        [Column(IsPrimary = true)] public string PDCode { get; set; } = "";

        public string PDName { get; set; } = "";
        public string PDSubName { get; set; } = "";
        public double PDStock { get; set; }

        public double PDSellPrice
        {
            get => _PDSellPrice;
            set
            {
                _PDSellPrice = value;
                if (_PDCost > 0) PDProfit = (value - _PDCost) / _PDCost;
            }
        }

        public double PDCost
        {
            get => _PDCost;
            set
            {
                _PDCost = value;
                if (value > 0) PDProfit = (_PDSellPrice - value) / value;
            }
        }

        public double PDProfit
        {
            get => _PDProfit;
            set
            {
                _PDProfit = value;
                OnPropertyChanged();
            }
        }

        public string PDSupplier { get; set; } = "";
        public string PDUnit { get; set; } = "";
        public bool IsVipDiscount { get; set; } = false;
        public bool IsVipPointInc { get; set; } = false;
        public bool IsWeighedNeeded { get; set; } = false;
        public bool IsCommunicationNeeded { get; set; } = false;
        public string PDGroup { get; set; } = "";
        public string WeighSN { get; set; } = "";
        public string PicPath { get; set; } = "";
    }
}