using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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


        private async void ConfigClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageConfig));
         

            //Thread myThread = new Thread(new ParameterizedThreadStart(ChangeProgressBar));
            //myThread.Start(50);
            //CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            //CancellationToken token = cancelTokenSource.Token;


            //var t1 = ProgressBar.Value;
            //var task = new Task(() => ProgressBar.Value = 10);
            //task.Start();
            ////ProgressBar.Value = 10;
            //var t = ProgressBar.Value;
            ////var action = new Action<double, CancellationToken>(ChangeProgressBar);
            //////var e1 =  new Task(()=>action(50,token));
            //////e1.Start();
            ////Console.WriteLine("working");
            //Thread.Sleep(3000);
            ////cancelTokenSource.Cancel();
            ////Console.WriteLine("end thread");
            ////new Task(() => EndChangeProgressBar(80)).Start();
            ////Console.WriteLine("End working");
            ////ProgressBar.Value = 30;
            //task = new Task(() => ProgressBar.Value = 50);
            //task.Start();

            //var progressIndicator = new Progress<int>(ReportProgress);
            //var e2 = await Foo(progressIndicator,10);
            //Thread.Sleep(5000);
            //e2 = await Foo(progressIndicator, 50);
        }

        //public async Task<int>  Foo(IProgress<int> progress,int val)
        //{
        //    var processed = await Task.Run<int>(() => { progress.Report(val); return 5; }
        //    );
        //    return 2;
        //}

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CheckConfig())
            {
                DisplayNoConfigDialogAsync();
                return;
            }

            if (viewModel.CheckInfo())
            {
                DisplayNoInformationDialogAsync();
                return;
            }

            viewModel.startGetInfo(ProgressBar, ChangeTextInTexBox);
        }



        //void ReportProgress(int value)
        //{
        //    Update the UI to reflect the progress value that is passed back.
        //    ProgressBar.Value = value;
        //}

        //private void ChangeProgressBar(double value, CancellationToken token)
        //{
        //    var t = ProgressBar.GetValue(NameProperty);
        //    while (ProgressBar.Value < value)
        //    {
        //        if (token.IsCancellationRequested)
        //        {
        //            return;
        //        }

        //        worker_ProgressChanged(ProgressBar, ProgressBar.Value + 1);
        //        Thread.Sleep(100);

        //    }
        //    Console.WriteLine("end working");
        //}
        //private void EndChangeProgressBar(object value)
        //{
        //    worker_ProgressChanged(ProgressBar, Convert.ToDouble(value));

        //    Console.WriteLine("end working2");
        //}


        //void worker_ProgressChanged(ProgressBar progressBar, double value)
        //{
        //    progressBar.Value = value;

        //}

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
            for(int i = 0; i < 3; i++) // переделать что бы мигал только пустой
            {
                City.Style = ErrorStyle;
                CityId.Style = ErrorStyle;
                TimeZone.Style = ErrorStyle;
                await Task.Delay(100);
                City.Style = BasicStyle;
                CityId.Style = BasicStyle;
                TimeZone.Style = BasicStyle;
                await Task.Delay(100);
            }
            
        }

        private void ChangeTextInTexBox(string text) //асинхронный метод может сделать?
        {
           TextBox.Text += Environment.NewLine + text;
        }

        //private void progressBar()
        //{
        //    int count = 0;
        //    while (count < 100)
        //    {
        //        Thread.Sleep(100);
        //        ProgressBar.Value += 1;
        //        count++;
        //    }
        //}



        private void ProgressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
         
        }


    }
}
