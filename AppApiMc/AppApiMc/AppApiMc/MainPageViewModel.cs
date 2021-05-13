using AppApiMc.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppApiMc
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private Setings setings;

        public Setings Setings
        {
            get => setings;
            set
            {
                setings = value;
                OnPropertyCahnged("Config");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyCahnged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public MainPageViewModel()
        {
            WorkWithSettings workWithSettings = new WorkWithSettings();

            Setings = workWithSettings.LoadSettings().Result;
        }

    }
}
