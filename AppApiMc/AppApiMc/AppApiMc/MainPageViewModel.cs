using AppApiMc.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AppApiMc
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private Setings setings;
        private WorkWithSettings workWithSettings;
        private MainPageModel mainPageModel;
        public MainPageModel MainPageModel
        {
            get => mainPageModel;
            set
            {
                mainPageModel = value;
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
            mainPageModel = new MainPageModel();
            workWithSettings = new WorkWithSettings();
            mainPageModel = new MainPageModel();
            ReadConfigAsync();
            
        }
        private async void ReadConfigAsync()
        {
            setings = await workWithSettings.LoadSettings();
        }
        public bool CheckConfig()
        {
            return mainPageModel.CheckSettings(setings);
        }
        public bool CheckInfo()
        {
            return mainPageModel.CheckInfo();
        }
        
        public void startGetInfo(ProgressBar progressBar)
        {

        }

    }
}
