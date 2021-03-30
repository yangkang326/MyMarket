﻿using System.Collections.ObjectModel;
using MyLib;
using Newtonsoft.Json;
using RestSharp;

namespace MyMarket.DbOperate
{
    public static class Operate
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
            var client = new RestClient($"https://localhost:44324/api/Cargos/GgeCargosByGroupname?s={GroupName}");
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
    }
}