using System.Collections.ObjectModel;
using Newtonsoft.Json;
using RestSharp;

namespace MyLib
{
    public static class WebApiOperate
    {
        //http://39.104.103.234:8081/api/Cargos/
        public static ObservableCollection<CargoInfoModel> GetCargoInfoModels()
        {
            string searchstring = "";
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/GetCargosBySerchstring?searchstr={searchstring}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.GET);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content);
        }


        public static ObservableCollection<CargoInfoModel> InserOrUpdateCargo(CargoInfoModel cargo)
        {
            var Client = new RestClient("http://39.104.103.234:8081/api/Cargos/InsertOrUpdateCargo")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            Request.AddHeader("Content-Type", "application/json");
            Request.AddParameter("application/json", JsonConvert.SerializeObject(cargo), ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content);
        }

        public static ObservableCollection<CargosGroup> InserOrUpdateGroup(string groupname)
        {
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/InsertOrUpdateCargoGroup?newGroupname={groupname}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(Response.Content);
        }

        public static ObservableCollection<CargosGroup> GetAllGroup()
        {
            var Client = new RestClient("http://39.104.103.234:8081/api/Cargos/GetAllGroup")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.GET);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargosGroup>>(Response.Content);
        }

        public static ObservableCollection<CargoInfoModel> DeleCargo(string pdcode)
        {
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/DeleCargo?pdcode={pdcode}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.DELETE);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<ObservableCollection<CargoInfoModel>>(Response.Content);
        }

        public static double CheckStock(string pdcode)
        {
            var Client = new RestClient($"http://39.104.103.234:8081/api/Cargos/CheckStock?pdcode={pdcode}")
            {
                Timeout = -1
            };
            var Request = new RestRequest(Method.POST);
            Request.AddParameter("text/plain", "", ParameterType.RequestBody);
            var Response = Client.Execute(Request);
            return JsonConvert.DeserializeObject<double>(Response.Content);
        }

        public static void UpLoadPic(string filePatch)
        {
            var client = new RestClient("http://39.104.103.234:8081/api/Cargos/UploadFile/UploadFile");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("file", filePatch);
            IRestResponse response = client.Execute(request);
        }
    }
}