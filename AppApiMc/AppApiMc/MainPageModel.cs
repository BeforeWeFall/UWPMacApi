using AppApiMc.Config;
using AppApiMc.JsonClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace AppApiMc
{
    class MainPageModel : INotifyPropertyChanged
    {

        private string city, idCity, timeZone;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        public string IdCity
        {
            get => idCity;
            set
            {
                idCity = value;
                OnPropertyChanged("IdCity");
            }
        }
        public string TimeZone
        {
            get => timeZone;
            set
            {
                timeZone = value;
                OnPropertyChanged("TimeZone");
            }
        }

        private Response response;
        private StorageFolder folder;




        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool CheckSettings(Setings setings)
        {
            return string.IsNullOrWhiteSpace(setings.Login) || string.IsNullOrWhiteSpace(setings.Password) || string.IsNullOrWhiteSpace(setings.PathToJsonId) || string.IsNullOrWhiteSpace(setings.PathToLoadId);
        }
        public bool CheckInfo()
        {
            return string.IsNullOrWhiteSpace(idCity) || string.IsNullOrWhiteSpace(timeZone) || string.IsNullOrWhiteSpace(city);     
        }
        
        public async Task PrepearResponc(Setings setings)
        {
            JsonInfo js = await ReadJsonAsync(setings.PathToJsonId);
            response = new Response(setings.Proxy, js,setings.Login,setings.Password);
        }


        public async Task GetToken()
        {
            int timespan = response.ConvertToUnixTimestamp(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            var taskRrq = response.HttpReqAsync((int)enumReq.Token, timespan, idcity: idCity, timeZ: timeZone);
            await taskRrq;
            //response.TryWaitResult(taskRrq);
        }
        public async Task GetCities(string idFolderToSave)
        {
            var taskRrq = response.HttpReqAsync((int)enumReq.Cities, 0, idcity: idCity, timeZ: timeZone);
            var res = await response.TryWaitResult(taskRrq);
            SerializeAndSave(idFolderToSave, "City.json", JsonSerializer.Deserialize<CityInfo>(res));
           
        }
        public async Task GetProduct(string idFolderToSave)
        {
            var taskRrq = response.HttpReqAsync((int)enumReq.Product, 0, idcity: idCity, timeZ: timeZone);
            var res = await response.TryWaitResult(taskRrq);
            SerializeAndSave(idFolderToSave, "Products.json", JsonSerializer.Deserialize<Products>(res));
        }
        public async Task<ReustoranInfo> GetRestourans(string idFolderToSave)
        {
            var taskRrq = response.HttpReqAsync((int)enumReq.Resturan, 0, idcity: idCity, timeZ: timeZone);
            var res = await response.TryWaitResult(taskRrq);
            SerializeAndSave(idFolderToSave, "Restorans.json", JsonSerializer.Deserialize<ReustoranInfo>(res));
            return JsonSerializer.Deserialize<ReustoranInfo>(res);
        }
        public async Task GetCatalog(string idFolderToSave)
        {
            var taskRrq = response.HttpReqAsync((int)enumReq.Catalog, 0, idcity: idCity, timeZ: timeZone);
            var res = await response.TryWaitResult(taskRrq);
            SerializeAndSave(idFolderToSave, "Catalog.json", JsonSerializer.Deserialize<Catalog>(res));
        }
        public async Task GetTokenRest(int timespan)
        {

            var taskRrq = response.HttpReqAsync((int)enumReq.TokenRest, timespan, idcity: idCity, timeZ: timeZone);
            var res = await response.TryWaitResult(taskRrq);
        }
        public async Task GetPrices()
        {
            int timespan = response.ConvertToUnixTimestamp(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            var taskRrq = response.HttpReqAsync((int)enumReq.Price, timespan, idcity: idCity, timeZ: timeZone);
            var res = await response.TryWaitResult(taskRrq);
        }
        public async Task<PricesInRestoran> GetPriceRest(string idFolderToSave, string idRest, string fileName)
        {
            int timespan = response.ConvertToUnixTimestamp(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            var taskRrq = response.HttpReqAsync((int)enumReq.PriceRest, timespan, idRest, idCity, timeZone);
            var res = await response.TryWaitResult(taskRrq);
            SerializeAndSave(idFolderToSave, fileName+".json", JsonSerializer.Deserialize<PricesInRestoran>(res));
            return JsonSerializer.Deserialize<PricesInRestoran>(res);
        }

        
        private async Task<JsonInfo> ReadJsonAsync(string fileid)
        {
            string text = "";
            StorageFile storageFile = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(fileid);
            using (var stream = await storageFile.OpenSequentialReadAsync())
            {
                using (StreamReader streamReader = new StreamReader(stream.AsStreamForRead()))
                {
                    text = streamReader.ReadToEnd();
                }
            }
            return JsonSerializer.Deserialize<JsonInfo>(text);
        }
        private async void SerializeAndSave<T>( string idfolder, string nameFile, T content)
        {
             folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(idfolder);
            StorageFile file = await folder.CreateFileAsync(nameFile,CreationCollisionOption.ReplaceExisting);
            
            using (Stream stream = await file.OpenStreamForWriteAsync())
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                await JsonSerializer.SerializeAsync(stream, content, options);
               
            }

            //using (FileStream fs = new FileStream(nameFile, FileMode.OpenOrCreate))
            //{
            //    var options = new JsonSerializerOptions
            //    {
            //        Encoder = JavaScriptEncoder.Create(UnicodeRanges., UnicodeRanges.Cyrillic),
            //        WriteIndented = true
            //    };
            //    var e = JsonSerializer.SerializeAsync(fs, content, options);
            //    e.Wait();
            //    Console.WriteLine("Data has been saved to file");
            //}
        }
    }
}
