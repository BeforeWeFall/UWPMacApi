using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppApiMc.Config
{
    class Setings : INotifyPropertyChanged
    {

        private string pathToJson, pathToLoad, proxy, login, password, pathToJsonid, pathToLoadId;


        public string PathToJson
        {
            get => pathToJson;
            set
            {
                pathToJson = value;
                OnPropertyChanged("PathToJson");
            }
        }

        public string PathToJsonId
        {
            get => pathToJsonid;
            set
            {
                pathToJsonid = value;
                OnPropertyChanged("PathToJsonId");
            }
        }

        public string PathToLoadId
        {
            get => pathToLoadId;
            set
            {
                pathToLoadId = value;
                OnPropertyChanged("PathToLoadId");
            }
        }
        public string PathToLoad
        {
            get => pathToLoad;
            set
            {
                pathToLoad = value;
                OnPropertyChanged("PathToLoad");
            }
        }
        public string Proxy
        {
            get => proxy;
            set
            {
                proxy = value;
                OnPropertyChanged("Proxy");
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}