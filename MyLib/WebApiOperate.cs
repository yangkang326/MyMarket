using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyLib
{
    public static class WebApiOperate
    {

        public static ObservableCollection<CargoInfoModel> StatiCargoInfoModels { get; set; }
        public static ObservableCollection<CargosGroup> StatiCargosGroups { get; set; }
        public static ObservableCollection<CargoUnit> StatiCargosUnits { get; set; }

        public static Task<ObservableCollection<CargoInfoModel>> GetAllCargoInfoModels()
        {
            var client = new RestClient("http://39.104.103.234:8081/api/Cargos/GetAllCargos");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var Response = client.Execute(request);
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content));
        }


        public  static  Task<ObservableCollection<CargoUnit>> GetAllUnit()
        {
            var client = new RestClient("http://39.104.103.234:8081/api/Cargos/GetAllUnits");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var Response = client.Execute(request);
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<CargoUnit>>(Response.Content));
        }


        public  static Task<ObservableCollection<CargoInfoModel>> InserOrUpdateCargo(CargoInfoModel cargo)
        {
            var Client = new RestClient("http://39.104.103.234:8081/api/Cargos/InsertOrUpdateCargo")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            Request.AddHeader("Content-Type", "application/json");
            Request.AddParameter("application/json", JsonConvert.SerializeObject(cargo), ParameterType.RequestBody);
            var Response =  Client.Execute(Request);
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content));
        }

        public  static Task<ObservableCollection<CargosGroup>> InserOrUpdateGroup(string groupname)
        {
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/InsertOrUpdateCargoGroup?newGroupname={groupname}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            var Response = Client.Execute(Request);
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(Response.Content));
        }

        public  static Task<ObservableCollection<CargoUnit>> InserOrUpdateunit(string unit)
        {
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/InsertOrUpdateCargoUnit?newunit={unit}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            var Response = Client.Execute(Request);
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<CargoUnit>>(Response.Content));
        }

        public  static Task<ObservableCollection<CargosGroup>> GetAllGroup()
        {
            var Client = new RestClient("http://39.104.103.234:8081/api/Cargos/GetAllGroup")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.GET);
            var Response =Client.Execute(Request);
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(Response.Content));
        }

        public  static Task<ObservableCollection<CargoInfoModel>> DeleCargo(string pdcode)
        {
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/DeleCargo?pdcode={pdcode}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            var Response =Client.Execute(Request);
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content));
        }

        public  static Task<double> CheckStock(string pdcode)
        {
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/CheckStock?pdcode={pdcode}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            var Response = Client.Execute(Request);
            return Task.FromResult(JsonConvert.DeserializeObject<double>(Response.Content));
        }

        public static void UpLoadPic(string filePatch)
        {
            var client = new RestClient("http://39.104.103.234:8081/api/Cargos/UploadFile/UploadFile");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("file", filePatch);
            var response =client.Execute(request);
        }
    }
}