using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace AppApiMc.Config
{
    class WorkWithSettings
    {
        private const string fileName = @"\Config\Setings.json";
        //private static StorageFolder settingsFolder = ApplicationData.Current.LocalFolder;
        private string settingsFolder = Directory.GetCurrentDirectory();
        public Setings LoadSettings()
        {
            try
            {
                string path = settingsFolder + fileName;
                string jsonString = File.ReadAllText($"{settingsFolder}\\{fileName}");
                var t = JsonSerializer.Deserialize<Setings>(jsonString);
                return t;

                //StorageFile sf = await settingsFolder.GetFileAsync(fileName);
                //if (sf == null) return null;

                //string content = await FileIO.ReadTextAsync(sf);
                //return JsonConvert.DeserializeObject<Setings>(content);
            }
            catch
            { return null; }
        }
        public bool SaveSettings(Setings data)
        {
            try
            {

                using (FileStream fs = new FileStream($"{settingsFolder}\\{fileName}", FileMode.OpenOrCreate))
                {
                    var options = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                        WriteIndented = true
                    };
                    var e = JsonSerializer.SerializeAsync(fs, data, options);
                }

                //StorageFile file = await settingsFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                //string content = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                //await FileIO.WriteTextAsync(file, content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
