using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using MovieGeek.a1.Model;

namespace MovieGeek.a1.Helpers
{
    internal static class ShuffleHelper
    {
        internal static Movie Shuffle(this IEnumerable<Movie> movies)
        {
            try
            {
                var moviesArray = movies.ToArray();
                var index = Rand(0, moviesArray.Count());
                return moviesArray[index];
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        internal static Int32 Rand(Int32 minVal, Int32 maxVal)
        {
            var rand = new Random();
            return rand.Next(minVal, maxVal);
        }

        internal static async Task FlowController(this FrameworkElement fx, IEnumerable<Movie> movies, SynchronizationContext syncContext)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(Rand(7, 12))).ContinueWith(task => syncContext.Send(callback =>
                {
                    var storyBd = fx.FindName("Update") as Storyboard;
                    if (storyBd != null)
                        storyBd.Begin();
                    fx.DataContext = movies.Shuffle();
                }, null));
            }
        }
    }
}