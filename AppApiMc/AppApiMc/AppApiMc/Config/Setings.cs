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

        public bool CheckPathToJson()
        {
            pathToJson = Regex.Replace(pathToJson.Trim(), @"^[^a-zA-Z0-9]*", "");
            bool t = File.Exists(PathToJson);
            return t;
        }
        public bool CheckPathDownload()
        {
            if (!Directory.Exists(PathToLoad))
                try
                {
                    Directory.CreateDirectory(pathToLoad);
                }
                catch
                {
                    return false;
                }
            return true;
        }
    }
}