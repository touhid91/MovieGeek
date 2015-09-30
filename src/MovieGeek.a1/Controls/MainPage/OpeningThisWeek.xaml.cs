using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MovieGeek.a1.Helpers;
using MovieGeek.a1.Model;
using System.Windows.Navigation;

namespace MovieGeek.a1.Controls.MainPage
{
    public partial class OpeningThisWeek : UserControl
    {
        public SynchronizationContext SynchronizationContext
        {
            get;
            set;
        }
        public OpeningThisWeek()
        {
            InitializeComponent();
            SynchronizationContext = SynchronizationContext.Current;
        }

        private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsHitTestVisible = false;
            ObservableCollection<Movie> movies = null;
            if (IsolatedStorageHelper.FileExists(IsoDir.OpeningThisWeek))
            {
                await IsolatedStorageHelper.ReadAsync(IsoDir.OpeningThisWeek).ContinueWith(task =>
                {
                    movies = task.Result.CreateDocument().CleanUp().ParseUpcoming();
                    if (!movies.Equals(null))
                    {
                        SynchronizationContext.Send(callback =>
                        {
                            Loaded.Stop();
                            background.Visibility = Visibility.Collapsed;
                            PlaceHolder.Visibility = Visibility.Collapsed;
                            this.IsHitTestVisible = true;
                            this.DataContext = movies.FirstOrDefault();
                        }, null);
                    }
                }, null);
            }
            using (var client = new HttpClient())
            {
                await client.GetStringAsync(UriDictionary.InTheatersUri).ContinueWith(async task =>
                {
                    if (task.IsFaulted)
                        return;
                    movies = null;
                    movies = task.Result.CleanUp().CreateDocument().CleanUp().ParseUpcoming();
                    if (!movies.Equals(null))
                    {
                        SynchronizationContext.Send(callback =>
                        {
                            Loaded.Stop();
                            background.Visibility = Visibility.Collapsed;
                            PlaceHolder.Visibility = Visibility.Collapsed;
                            this.IsHitTestVisible = true;
                            this.DataContext = movies.FirstOrDefault();
                            LayoutRoot.UpdateLayout();
                        }, null);
                    }
                    await IsolatedStorageHelper.WriteAsync(IsoDir.OpeningThisWeek, task.Result.CleanUp());
                }).ContinueWith(async task => await this.FlowController(movies, SynchronizationContext));
            }
        }

        private void textBlock_LayoutUpdated(object sender, EventArgs e)
        {
            textBlock_ManipulationDelta(sender, null);
        }

        private void textBlock_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {

        }

        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}