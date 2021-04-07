using System.Collections.ObjectModel;
using MyLib;

namespace MyMarket
{
    public static class WindowsStatus
    {
        public static bool MainWindowOpen { get; set; } = false;
        public static bool MenuWindowOpen { get; set; } = false;
        public static bool CargoCheckWindowOpen { get; set; } = false;
        public static bool CargoEditWindowOpen { get; set; } = false;
        public static ObservableCollection<CargoInfoModel> StatiCargoInfoModels { get; set; }
        public static ObservableCollection<CargosGroup> StatiCargosGroups { get; set; }
    }
}