using Ninject;
using Ninject.Modules;
using Xamarin.Forms;
using XamarinBase.Interfaces;
using XamarinBase.Modules;
using XamarinBase.Startup;
using XamarinBase.ViewModels;

namespace XamarinBase
{
    public partial class App : Application
    {
        /// <summary>
        /// Gets or sets the application loader.
        /// </summary>
        /// <value>
        /// The application loader.
        /// </value>
        public AppLoader AppLoader { get; set; }

        /// <summary>
        /// Initializes a new instance of the app.
        /// </summary>
        /// <param name="platformModules">The platoform specific modules that need to be loaded into the kernal.</param>
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            AppLoader = new AppLoader();
            AppLoader.Startup<MainViewModel>(new MainPage(), platformModules);
            MainPage = AppLoader.MainPage;
        }

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
