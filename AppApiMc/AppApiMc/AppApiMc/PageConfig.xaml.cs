using System;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            //bool status=true;  ПЕРЕДЕЛАТЬ Т.К. ПРОВЕРКА ТЕПЕРЬ НЕ РАОТАЕТ СУКК

            //if (!viewModel.CheckJson())
            //{
            //    PathJson.Text += " файл не обнаружен";
            //    PathJson.Background = new SolidColorBrush(Colors.Red);
            //    status = false;
            //}
            //else
            //    PathJson.Background = new SolidColorBrush(Colors.White);
            //if (! viewModel.CheckJson())
            //{
            //    PathLoad.Text += " директория не обнаружена";
            //    PathLoad.Background = new SolidColorBrush(Colors.Red);
            //    status = false;
            //}
            //else
            //    PathJson.Background = new SolidColorBrush(Colors.White);

            //viewModel.SaveConfig();

            //if (status)
            //    Frame.Navigate(typeof(MainPage));
            viewModel.SaveConfig();
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void Back2_Click(object sender, RoutedEventArgs e)
        {
            //FileOpenPicker
            
            FolderPicker folderPicker = new FolderPicker();
            
            folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            
            if (folder != null)
            {
                
                viewModel.Config.PathToLoad = StorageApplicationPermissions.FutureAccessList.Add(folder);
                PathLoad.Text = folder.Path;
               
                ////  do Things On Folder
                //string name = "myTitle.txt";
                //await folder.CreateFileAsync(name, CreationCollisionOption.GenerateUniqueName); ЧЕРЕЗ ЭТ ХУЙНЮ ВЫБРАТЬ ПАПКУ КУДА СОХРАНЯЕМ АХУЕТЬ ПИЗДЕЦ БЛЯТЬ ПОИГРАТЬСЯ ПОТОМ С СОХРАНЕНИЕ ФАЙЛА ПИСОС
            }
            else
            {
                MessageDialog dialog = new MessageDialog("you selected nothing");
                await dialog.ShowAsync();
            }
        }
    }
}
