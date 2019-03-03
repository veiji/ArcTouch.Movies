using ArcTouch.Movies.Helpers;
using ArcTouch.Movies.Helpers.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcTouch.Movies.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        #region Fields
        readonly Func<string, string, string, Task> displayAlertFunc;
        #endregion

        public ViewModelBase(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc)
        {
            Navigation = navigation;
            this.displayAlertFunc = displayAlertFunc;
        }

        protected INavigationService Navigation { get; private set; }


        private bool isBusy;
        public bool IsBusy
        {
            get{return isBusy;}
            set{SetProperty(ref isBusy, value);}
        }

        /// <summary>
        /// Presents an alert dialog to the application user with a single cancel button.
        /// </summary>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="message">The body text of the alert dialog.</param>
        /// <param name="cancel">Text to be displayed on the 'Cancel' button.</param>
        public Task DisplayAlertAsync(string title, string message, string cancel)
        {
            return DeviceExtension.InvokeOnMainThreadAsync(() => { return displayAlertFunc(title, message, cancel); });
        }

    }
}
