using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyLib;

namespace MyMarkerWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupNameController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public GroupNameController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ObservableCollection<CargoInfoModel> GET()
        {
            return new()
            {
                new() {ID = 1}, new() {ID = 2, PDCode = "123"}
            };
        }
    }
}