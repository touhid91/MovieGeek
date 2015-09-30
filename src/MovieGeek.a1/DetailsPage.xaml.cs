using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Net.Http;
using MovieGeek.a1.Helpers;
using System.Threading;

namespace MovieGeek.a1
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        public DetailsPage()
        {
            InitializeComponent();
            this.Title = "MOVIE DETAILS";
            this.SyncContext = SynchronizationContext.Current;
        }

        public String RefUrl { get; set; }
        public SynchronizationContext SyncContext { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (this.NavigationContext.QueryString.ContainsKey("movie"))
            {
                RefUrl = NavigationContext.QueryString["movie"];
            }
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                if (!RefUrl.Equals(null))
                {
                    await client.GetStringAsync(String.Format("http://www.imdb.com/title/{0}/", RefUrl)).ContinueWith(task =>
                    {
                         var movie = task.Result.CleanUp().CreateDocument().CleanUp().ParseMovie();
                        SyncContext.Send(callback =>
                        {
                            this.DataContext = movie;

                            this.UpdateLayout();
                        }, null);
                    });
                }

            }
        }
    }
}