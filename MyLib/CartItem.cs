using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MyLib
{
    public class CartItem:ObservableObject
    {
        private double _Count;

        private double _PDTotalPrice;
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