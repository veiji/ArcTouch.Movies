using ArcTouch.Movies.Helpers;
using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ArcTouch.Movies
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationService.Configure(Constants.MAINPAGE, typeof(MainPage));
            NavigationService.Configure(Constants.DETAILSPAGE, typeof(MovieDetails));

            MainPage = ((ViewNavigationService)NavigationService).SetRootPage(Constants.MAINPAGE);
        }

        public static INavigationService NavigationService { get; } = new ViewNavigationService();

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
