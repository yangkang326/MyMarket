using System.Collections.ObjectModel;
using Newtonsoft.Json;
using RestSharp;

namespace MyLib
{
    public static class WebApiOperate
    {
        public static ObservableCollection<CargoInfoModel> GetCargoInfoModels(string Searchstring)
        {
            var client = new RestClient($"https://localhost:44324/api/Cargos/GetCargosBySerchstring?s={Searchstring}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(response.Content);
        }

        public static ObservableCollection<CargoInfoModel> GetCargoInfoModelsByGroupName(string GroupName)
        {
            var client =
                new RestClient($"https://localhost:44324/api/Cargos/GetCargosByGroupname?groupname={GroupName}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(response.Content);
        }

        public static ObservableCollection<CargoInfoModel> InserOrUpdateCargo(CargoInfoModel cargo)
        {
            var client = new RestClient("https://localhost:44324/api/Cargos/InsertOrUpdateCargo");
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
                new RestClient($"https://localhost:44324/api/Cargos/InsertOrUpdateCargoGroup?newGroupname={groupname}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(response.Content);
        }

        public static ObservableCollection<CargosGroup> GetAllGroup()
        {
            var client = new RestClient("https://localhost:44324/api/Cargos/GetAllGroup");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(response.Content);
        }

        public static ObservableCollection<CargoInfoModel> DeleCargo(string pdcode)
        {
            var client = new RestClient($"https://localhost:44324/api/Cargos/DeleCargo?pdcode={pdcode}");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(response.Content);
        }

        public static double CheckStock(string pdcode)
        {
            var client = new RestClient($"https://localhost:44324/api/Cargos/CheckStock?pdcode={pdcode}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<double>(response.Content);
        }
    }
}