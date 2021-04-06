namespace MyMarketMobile.Models
{
    public class CargoInfoModel
    {
        public string _PDCode = "";
        private double _PDCost;
        private double _PDProfit;
        private double _PDSellPrice;

        public int ID { get; set; }

        public string PDCode
        {
            get => _PDCode;
            set
            {
                _PDCode = value;
            }
        }

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
            get ;
            set

   ;

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