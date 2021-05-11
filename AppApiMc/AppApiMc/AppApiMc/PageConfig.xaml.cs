using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppApiMc
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PageConfig : Page
    {
        ConfigViewModel viewModel;
        public PageConfig()
        {
            viewModel = new ConfigViewModel();
            this.InitializeComponent();

            DataContext = viewModel;
        }

        private void Append(object sender, RoutedEventArgs e)
        {
            bool status=true;

            if (!viewModel.CheckJson())
            {
                PathJson.Text += " файл не обнаружен";
                PathJson.Background = new SolidColorBrush(Colors.Red);
                status = false;
            }
            else
                PathJson.Background = new SolidColorBrush(Colors.White);
            if (!viewModel.CheckJson())
            {
                PathLoad.Text += " директория не обнаружена";
                PathLoad.Background = new SolidColorBrush(Colors.Red);
                status = false;
            }
            else
                PathJson.Background = new SolidColorBrush(Colors.White);

            viewModel.SaveConfig();

            if (status)
                Frame.Navigate(typeof(MainPage));
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
