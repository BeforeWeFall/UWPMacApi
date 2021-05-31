using AppApiMc.Config;
using AppApiMc.JsonClasses;
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
        
        public async void startGetInfo(ProgressBar progressBar, Action<string> updateText)
        {
            await mainPageModel.PrepearResponc(setings);
            updateText("Производим настройки");
            await mainPageModel.GetToken();
            updateText("Получаем токе");
            await mainPageModel.GetCities(setings.PathToLoadId);
            updateText("Получаем список городов");
            await mainPageModel.GetProduct(setings.PathToLoadId);
            updateText("Получаем список продуктов");
            await mainPageModel.GetCatalog(setings.PathToLoadId);
            updateText("Получаем каталог");
            var restoran = await mainPageModel.GetRestourans(setings.PathToLoadId);
            updateText("Получаем список ресторанов");
            progressBar.Value = 5;

            var ourRestoran = restoran.items.Where(x => x.city.Trim().Equals(mainPageModel.IdCity)).Select(q => q).ToList();

            var step = (progressBar.Maximum - progressBar.Value) / ourRestoran.Count;

            foreach(var rest in ourRestoran)
            {
                updateText($"Получаем сведенья о ресторане {rest.address}");
                await mainPageModel.GetToken();
                await mainPageModel.GetPrices();
                await mainPageModel.GetPriceRest(setings.PathToLoadId, rest.id, rest.name);
                progressBar.Value += step;
            }   

            //var ourCity = cities.items.Where(X=>X.name.Equals(mainPageModel.City)).Select(q=>q).ToList();

        }
        
    }
}
