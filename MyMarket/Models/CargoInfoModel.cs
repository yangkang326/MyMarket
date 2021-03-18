using FreeSql.DataAnnotations;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MyMarket.Models
{
    public class CargoInfoModel : ObservableObject
    {
        private string _PDCode = "";
        private double _PDProfit;

        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        [Column(IsNullable = false)]
        public string PDCode
        {
            get => _PDCode;
            set
            {
                _PDCode = value;
                OnPropertyChanged();
            }
        }

        [Column(IsNullable = false)] public string PDName { get; set; } = "";

        [Column(IsNullable = false)] public string PDSubName { get; set; } = "";

        [Column(IsNullable = false)] public double PDStock { get; set; }

        [Column(IsNullable = false)] public double PDSellPrice { get; set; }

        [Column(IsNullable = false)] public double PDCost { get; set; }

        [Column(IsNullable = false)]
        public double PDProfit
        {
            get => _PDProfit;
            set
            {
                _PDProfit = value;
                OnPropertyChanged();
            }
        }

        [Column(IsNullable = true)] public string PDSupplier { get; set; } = "";

        [Column(IsNullable = true)] public string PDUnit { get; set; } = "";

        [Column(IsNullable = true)] public bool IsVipDiscount { get; set; } = false;

        [Column(IsNullable = true)] public bool IsVipPointInc { get; set; } = false;

        [Column(IsNullable = true)] public bool IsWeighedNeeded { get; set; } = false;

        [Column(IsNullable = true)] public bool IsCommunicationNeeded { get; set; } = false;
        public string PDGroup { get; set; } = "";
        [Column(IsNullable = true)] public string WeighSN { get; set; } = "";
        [Column(IsNullable = true)] public string PicPath { get; set; } = "";
    }
}