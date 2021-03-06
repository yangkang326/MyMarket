using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MyMarket.Pay.ViewModel
{
    public class PayViewModel : ObservableObject
    {
        private double _CargosCost;

        private double _Exchange;

        private double _Pay;

        public double CargosCost
        {
            get => _CargosCost;
            set
            {
                _CargosCost = value;
                OnPropertyChanged();
            }
        }

        public double Pay
        {
            get => _Pay;
            set
            {
                _Pay = value;
                _Exchange = value - _CargosCost > 0 ? value - _CargosCost : 0;
                OnPropertyChanged();
            }
        }

        public double Exchange
        {
            get => _Exchange;
            set
            {
                _Exchange = value;
                OnPropertyChanged();
            }
        }
    }
}