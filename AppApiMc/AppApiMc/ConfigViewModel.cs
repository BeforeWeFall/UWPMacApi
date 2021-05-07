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

        private itemConfig config;

        public itemConfig Config 
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



        public ConfigViewModel()
        {
            Config = JsonDeserialize();
        }
        private itemConfig JsonDeserialize()
        {
            string jsonString = File.ReadAllText(@"Config\Setings.json");
            var t = JsonSerializer.Deserialize<itemConfig>(jsonString);
            return t;
        }
    }
}
