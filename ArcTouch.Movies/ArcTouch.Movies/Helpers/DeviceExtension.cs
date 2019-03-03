using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcTouch.Movies.Helpers
{
    public static class DeviceExtension
    {
        public static Task InvokeOnMainThreadAsync(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }

            });

            return tcs.Task;
        }

        public static Task InvokeOnMainThreadAsync(Func<Task> action)
        {
            var tcs = new TaskCompletionSource<Task>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    tcs.TrySetResult(action());
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }

            });

            return tcs.Task;
        }
    }
}
