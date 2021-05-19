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

        public void StartJob(Setings setings)
        {

        }
    }
}
