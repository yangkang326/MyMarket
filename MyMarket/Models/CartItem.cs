using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MyMarket.Models
{
    public class CartItem:ObservableObject
    {
        private double _Count;
        public string PDName { get; set; }
        public string PDSN { get; set; }

        public double UnitPrice { get; set; }

        public double Count
        {
            get => _Count;
            set
            {
                _Count = value;
                PDTotalPrice = value * UnitPrice;
            }
        }

        private double _PDTotalPrice;

        public double PDTotalPrice
        {
            get => _PDTotalPrice;
            set
            {
                _PDTotalPrice = value;
                OnPropertyChanged();
            }
        }
    }
}