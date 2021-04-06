using System.Collections.ObjectModel;
using Newtonsoft.Json;
using RestSharp;

namespace MarketMoblie.Model
{
    public static class WebApiOperate
    {
        public static ObservableCollection<CargoInfoModel> GetCargoInfoModels(string searchstring)
        {
            var Client =
                new RestClient($"http://localhost:28294/api/Cargos/GetCargosBySerchstring?searchstr={searchstring}");
            Client.Timeout = -1;
            var Request = new RestRequest(Method.GET);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content);
        }

        public static ObservableCollection<CargoInfoModel> GetCargoInfoModelsByGroupName(string groupName)
        {
            var Client =
                new RestClient($"http://localhost:28294/api/Cargos/GetCargosByGroupname?groupname={groupName}");
            Client.Timeout = -1;
            var Request = new RestRequest(Method.GET);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content);
        }

        public static ObservableCollection<CargoInfoModel> InserOrUpdateCargo(CargoInfoModel cargo)
        {
            var Client = new RestClient("http://localhost:28294/api/Cargos/InsertOrUpdateCargo");
            Client.Timeout = -1;
            var Request = new RestRequest(Method.POST);
            Request.AddHeader("Content-Type", "application/json");
            Request.AddParameter("application/json", JsonConvert.SerializeObject(cargo), ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content);
        }

        public static ObservableCollection<CargosGroup> InserOrUpdateGroup(string groupname)
        {
            var Client =
                new RestClient($"http://localhost:28294/api/Cargos/InsertOrUpdateCargoGroup?newGroupname={groupname}");
            Client.Timeout = -1;
            var Request = new RestRequest(Method.POST);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(Response.Content);
        }

        public static ObservableCollection<CargosGroup> GetAllGroup()
        {
            var Client = new RestClient("http://localhost:28294/api/Cargos/GetAllGroup");
            Client.Timeout = -1;
            var Request = new RestRequest(Method.GET);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(Response.Content);
        }

        public static ObservableCollection<CargoInfoModel> DeleCargo(string pdcode)
        {
            var Client = new RestClient($"http://localhost:28294/api/Cargos/DeleCargo?pdcode={pdcode}");
            Client.Timeout = -1;
            var Request = new RestRequest(Method.DELETE);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content);
        }

        public static double CheckStock(string pdcode)
        {
            var Client = new RestClient($"http://localhost:28294/api/Cargos/CheckStock?pdcode={pdcode}");
            Client.Timeout = -1;
            var Request = new RestRequest(Method.POST);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<double>(Response.Content);
        }
    }
}