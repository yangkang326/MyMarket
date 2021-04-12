using MyLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MarkerWebBootstrapBlazor.Shared.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public Task<ObservableCollection<CargoInfoModel>> GetForecastAsync()
        {
            return Task.FromResult(WebApiOperate.GetAllCargoInfoModels().Result);
        }
    }
}
