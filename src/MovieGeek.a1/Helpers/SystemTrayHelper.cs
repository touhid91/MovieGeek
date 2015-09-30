using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Phone.Shell;

namespace MovieGeek.a1.Helpers
{
    internal static class SystemTrayHelper
    {
        internal static void SetTrayVisibleIndeterminent(this Page containerPage, String message, SynchronizationContext syncContext)
        {
            syncContext.Send(callback =>
            {
                SystemTray.SetIsVisible(containerPage, true);
                SystemTray.SetOpacity(containerPage, 0.5);
                var indicator = new ProgressIndicator {Text = message, IsVisible = true, IsIndeterminate = true};
                SystemTray.SetProgressIndicator(containerPage, indicator);
            }, null);
        }

        internal static Task SetTrayCollapsed(this Page containerPage, SynchronizationContext syncContext)
        {
            Thread.Sleep(2000);
            syncContext.Send(callback => SystemTray.SetIsVisible(containerPage, false), null);
            return null;
        }
    }
}