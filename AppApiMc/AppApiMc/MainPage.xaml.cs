
using System.IO;
using Windows.UI.Xaml.Controls;
using System.Text.Json;
using Windows.UI.Xaml;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AppApiMc
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

         

            this.InitializeComponent();
            DataContext = null;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
