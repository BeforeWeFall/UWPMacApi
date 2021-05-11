using AppApiMc.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            workWithSettings.SaveSettings(config);
        }

        public ConfigViewModel()
        {
            workWithSettings = new WorkWithSettings();

            Config = workWithSettings.LoadSettings();
        }
        public bool CheckJson()
        {
            return config.CheckPathToJson();
        }
        public bool CheckLoad()
        {
            return config.CheckPathDownload();
        }
        //private itemConfig JsonDeserialize()
        //{
        //    string jsonString = File.ReadAllText(@"Config\Setings.json");
        //    var t = JsonSerializer.Deserialize<itemConfig>(jsonString);
        //    return t;
        //}
    }
}
