using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AppApiMc
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel viewModel;
        public MainPage()
        {
            viewModel = new MainPageViewModel();
            this.InitializeComponent();
            DataContext = viewModel;
        }


        private void ConfigClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageConfig));
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!viewModel.CheckConfig())
            {
                DisplayNoConfigDialogAsync();
                return;
            }

            if (!viewModel.CheckInfo())
            {
                DisplayNoInformationDialogAsync();
                return;
            }

            viewModel.startGetInfo( ProgressBar)
        }

        private async void DisplayNoConfigDialogAsync()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "No information",
                Content = "Please go to config and write info",
                CloseButtonText = "Ok"

            };
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        private async void DisplayNoInformationDialogAsync()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "No information",
                Content = "Please write info",
                CloseButtonText = "Ok"
                // do some body intersting
            };
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
