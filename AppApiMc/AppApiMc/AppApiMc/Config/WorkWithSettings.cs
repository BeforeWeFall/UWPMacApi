using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace AppApiMc.Config
{
    class WorkWithSettings
    {
        private const string fileName = "Setings.json";
        private const string fileFolder = "Config";
        //private static StorageFolder settingsFolder = ApplicationData.Current.LocalFolder;
        private string settingsFolder = Directory.GetCurrentDirectory();
         


        public async Task<Setings> LoadSettings()
        {
            StorageFolder storageFolder =
                    ApplicationData.Current.LocalFolder;
            Setings json;
            try
            {
                StorageFile file = await storageFolder.GetFileAsync(fileName);
                //StorageFile file= await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///local/file.txt"));
                // var stream = await file.OpenStreamForReadAsync();
                using (var stream = await file.OpenSequentialReadAsync())
                {
                    string jsonString;
                    using (StreamReader stReader = new StreamReader(stream.AsStreamForRead()))
                    {
                        jsonString = stReader.ReadToEnd();
                    }
                    json = JsonSerializer.Deserialize<Setings>(jsonString);
                }

                //string jsonString;
                //StorageFile sampleFile =
                //    await storageFolder.GetFileAsync(fileName);
                //string jsonStringT = await FileIO.ReadTextAsync(sampleFile);


                ////var stream = await sampleFile.OpenAsync(FileAccessMode.Read);
                ////ulong size = stream.Size;
                ////using (var inputStream = stream.GetInputStreamAt(0))
                ////{
                ////    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                ////    {
                ////        uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                ////        jsonString = dataReader.ReadString(numBytesLoaded);
                ////    }
                ////}
                //json = JsonSerializer.Deserialize<Setings>(jsonStringT);

            }
            catch (FileNotFoundException e)
            {
                string path = settingsFolder + fileName;
                string jsonString = await File.ReadAllTextAsync($"{settingsFolder}\\{fileFolder}\\{fileName}");
                json = JsonSerializer.Deserialize<Setings>(jsonString);
                SaveSettingsLocalFolder(json);
            }
            return json;      
        }

        public async void SaveSettingsLocalFolder(Setings h)
        {
            StorageFolder storageFolder =
                    ApplicationData.Current.LocalFolder;
            StorageFile file =
             await storageFolder.CreateFileAsync("Setings.json", CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                await JsonSerializer.SerializeAsync(stream, h, options);               
            }
           
        }        
    }
}
