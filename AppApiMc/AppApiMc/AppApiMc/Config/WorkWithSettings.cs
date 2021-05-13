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
                var stream = await file.OpenStreamForReadAsync();
                string jsonString;
                using (StreamReader stReader = new StreamReader(stream))
                {
                    jsonString = stReader.ReadToEnd();
                }
                json = JsonSerializer.Deserialize<Setings>(jsonString);
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
