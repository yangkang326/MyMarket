using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MarketMoblie.Model
{
    public class CargoInfoModel : ObservableObject
    {
        private double _PdCost;
        private double _PdProfit;
        private double _PdSellPrice;
        public string PdCode = "";
        public int PDId { get; set; }

        public string PDCode
        {
            get => PdCode;
            set
            {
                PdCode = value;
                OnPropertyChanged();
            }
        }

        public string PDName { get; set; } = "";
        public string PDSubName { get; set; } = "";
        public double PDStock { get; set; }

        public double PDSellPrice
        {
            get => _PdSellPrice;
            set
            {
                _PdSellPrice = value;
                if (_PdCost > 0) PDProfit = (value - _PdCost) / _PdCost;
            }
        }

        public double PDCost
        {
            get => _PdCost;
            set
            {
                _PdCost = value;
                if (value > 0) PDProfit = (_PdSellPrice - value) / value;
            }
        }

        public double PDProfit
        {
            get => _PdProfit;
            set
            {
                _PdProfit = value;
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
        public string PDWeighSn { get; set; } = "";
        public string PicPath { get; set; } = "";
    }
}