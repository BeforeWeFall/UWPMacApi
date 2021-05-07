using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace AppApiMc
{
    class itemConfig : INotifyPropertyChanged
    {
        private string pathToJson, pathToLoad, proxy, login, password;

        public string PathToJson
        {
            get => pathToJson;
            set
            {
                pathToJson = value;
                OnPropertyChanged("PathToJson");
            }
        }
        public string PathToLoad {
            get => pathToLoad;
            set
            {
                pathToLoad = value;
                OnPropertyChanged("PathToLoad");
            }
        }
        public string Proxy {
            get => proxy;
            set
            {
                proxy = value;
                OnPropertyChanged("Proxy");
            }
        }
        public string Password {
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
        public void OnPropertyChanged([CallerMemberName] string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


    }
}
