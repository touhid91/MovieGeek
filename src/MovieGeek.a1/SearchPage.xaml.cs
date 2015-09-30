using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using MovieGeek.a1.Helpers;
using System.Threading;

namespace MovieGeek.a1
{
    public partial class SearchPage : PhoneApplicationPage
    {
        public SearchPage()
        {
            InitializeComponent();
            SyncC = SynchronizationContext.Current;
        }

        public SynchronizationContext SyncC { get; set; }
        private async void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SearchField.Text != null)
            {
                var s=String.Format("http://www.imdb.com/find?q={0}&s=all",SearchField.Text);
                using (var client = new HttpClient())
                {
                    await client.GetStringAsync(String.Format("http://www.imdb.com/find?q={0}&s=all",SearchField.Text)).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                            return;
                        var res = task.Result.CreateDocument().CleanUp().SearchParser();
                        SyncC.Send(callback =>
                        {
                            srch.ItemsSource = res;
                        }, null);
                    });
                }
            }
        }

        private void srch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender as LongListSelector).SelectedItem.Equals(null))
            {
                var selection = ((sender as LongListSelector).SelectedItem as SearchRes).SelfUri;
                NavigationService.Navigate(new Uri("/DetailsPage.xaml?movie=" + selection, UriKind.Relative));
            }
        }
    }

    public class SearchRes
    {
        public String PosterUri { get; set; }
        public String Title { get; set; }
        public String Extra { get; set; }
        public String SelfUri { get; set; }
    }
}