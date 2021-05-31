using AppApiMc.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Storage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;

namespace AppApiMc
{
    class ConfigViewModel : INotifyPropertyChanged
    {

        private Setings config;
        private WorkWithSettings workWithSettings;

        public Setings Config
        {
            get => config;
            set
            {
                config = value;
                OnPropertyCahnged("Config");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyCahnged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public void SaveConfig()
        {
            workWithSettings.SaveSettingsLocalFolder(config);
        }

        public ConfigViewModel()
        {
            workWithSettings = new WorkWithSettings();
            ReadCOnfigAsync();
          


            //if(StorageApplicationPermissions.FutureAccessList.ContainsItem(config.PathToJsonId))
            //Try(config.PathToJsonId);


        }
        public async void ReadCOnfigAsync()
        {
            Config = await workWithSettings.LoadSettings();
        }

        public void UpdatePathToJson(StorageFile file)
        {
            config.PathToJson = file.Path;
            config.PathToJsonId = StorageApplicationPermissions.FutureAccessList.Add(file);
        }
        public void UpdatePathToLoad(StorageFolder folder)
        {
            config.PathToLoad = folder.Path;
            config.PathToLoadId = StorageApplicationPermissions.FutureAccessList.Add(folder);
        }

        //private async void Try(string tk)
        //{
        //    var file = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(tk);
        //    StorageFile fileNew =  await file.CreateFileAsync("ter.txt", CreationCollisionOption.ReplaceExisting);


        //}
        //private itemConfig JsonDeserialize()
        //{
        //    string jsonString = File.ReadAllText(@"Config\Setings.json");
        //    var t = JsonSerializer.Deserialize<itemConfig>(jsonString);
        //    return t;
        //}
    }
}
