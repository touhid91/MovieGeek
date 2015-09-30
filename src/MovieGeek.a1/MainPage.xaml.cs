using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using MovieGeek.a1.Helpers;
using MovieGeek.a1.Model;
using System.Threading;
using System.Net.Http;

namespace MovieGeek.a1
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.SyncContext = SynchronizationContext.Current;
        }

        public SynchronizationContext SyncContext { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //if (OpeningThisWeek.DataContext.Equals(null))
            //{
            //    OpeningThisWeek.IsHitTestVisible = false;
            //}
        }

        private void OpeningThisWeek_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var link = (sender as UserControl).Tag;
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?movie=" + link, UriKind.Relative));
        }

        private void InTheaters_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var link = (sender as UserControl).Tag;
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?movie=" + link, UriKind.Relative));
        }

        private async void PanoramaItem_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Crew> stars = null;
            if (IsolatedStorageHelper.FileExists("STARmeter"))
            {
                await IsolatedStorageHelper.ReadAsync("STARmeter").ContinueWith(task =>
                    {
                        stars = task.Result.CreateDocument().ParseCrews();
                        SyncContext.Send(callback =>
                        {
                            CastsList.ItemsSource = stars;
                        }, null);
                    });
            }
            using (var client = new HttpClient())
            {
                await client.GetStringAsync(new Uri("http://www.imdb.com/search/name?gender=male,female&ref_=nv_cel_m_3")).ContinueWith(async task =>
                {
                    if (task.IsFaulted)
                        return;
                    stars = null;
                    stars = task.Result.CleanUp().CreateDocument().ParseCrews();
                    SyncContext.Send(callback =>
                    {
                        CastsList.ItemsSource = stars;
                        CastsList.UpdateLayout();
                    }, null);
                    await IsolatedStorageHelper.WriteAsync("STARmeter", task.Result);
                });
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));
        }
    }
}