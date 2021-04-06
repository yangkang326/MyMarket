using System.Collections.ObjectModel;
using MyMarketMobile.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MyMarketMobile.ViewModels
{
    public class WebApiOperate
    {
        public static ObservableCollection<CargoInfoModel> GetCargoInfoModels(string Searchstring)
        {
            var client =
                new RestClient($"http://localhost:28294/api/Cargos/GetCargosBySerchstring?searchstr={Searchstring}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(response.Content);
        }

        public static ObservableCollection<CargoInfoModel> GetCargoInfoModelsByGroupName(string GroupName)
        {
            var client =
                new RestClient($"http://localhost:28294/api/Cargos/GetCargosByGroupname?groupname={GroupName}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(response.Content);
        }

        public static ObservableCollection<CargoInfoModel> InserOrUpdateCargo(CargoInfoModel cargo)
        {
            var client = new RestClient("http://localhost:28294/api/Cargos/InsertOrUpdateCargo");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(cargo), ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(response.Content);
        }

        public static ObservableCollection<CargosGroup> InserOrUpdateGroup(string groupname)
        {
            var client =
                new RestClient($"http://localhost:28294/api/Cargos/InsertOrUpdateCargoGroup?newGroupname={groupname}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(response.Content);
        }

        public static ObservableCollection<CargosGroup> GetAllGroup()
        {
            var client = new RestClient("http://localhost:28294/api/Cargos/GetAllGroup");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(response.Content);
        }

        public static ObservableCollection<CargoInfoModel> DeleCargo(string pdcode)
        {
            var client = new RestClient($"http://localhost:28294/api/Cargos/DeleCargo?pdcode={pdcode}");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(response.Content);
        }

        public static double CheckStock(string pdcode)
        {
            var client = new RestClient($"http://localhost:28294/api/Cargos/CheckStock?pdcode={pdcode}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<double>(response.Content);
        }

    }
}