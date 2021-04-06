using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MyLib
{
    public class CartItem : ObservableObject
    {
        private double _Count;

        private double _PdTotalPrice;
        public string PDName { get; set; }
        public string PDSn { get; set; }

        public double UnitPrice { get; set; }

        public double Count
        {
            get => _Count;
            set
            {
                _Count = value;
                OnPropertyChanged();
                PDTotalPrice = value * UnitPrice;
            }
        }

        public double PDTotalPrice
        {
            get => _PdTotalPrice;
            set
            {
                _PdTotalPrice = value;
                OnPropertyChanged();
            }
        }
    }
}