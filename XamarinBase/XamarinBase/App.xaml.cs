using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using Xamarin.Forms;
using XamarinBase.Modules;

namespace XamarinBase
{
    public partial class App : Application
    {
        /// <summary>
        /// Gets or sets the dependency injection kernal.
        /// </summary>
        /// <value>
        /// The kernal.
        /// </value>
        public IKernel Kernal { get; set; }

        /// <summary>
        /// Initializes a new instance of the app.
        /// </summary>
        /// <param name="platofrmModules">The platoform specific modules that need to be loaded into the kernal.</param>
        public App(params INinjectModule[] platofrmModules)
        {
            InitializeComponent();
            // we must put some page, even if the page is nothing for iOS
            MainPage = new ContentPage(); 

            // Register all the modules with the kernal
            Kernal = new StandardKernel(new ServiceModule(), new NavigationModule(MainPage.Navigation));
        }

        protected override void OnStart()
        {
            // App is starting now so get the main page ready
            var mainPage = new NavigationPage(new MainPage());
            // set the binding context/view model for this page

            MainPage = mainPage;
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
