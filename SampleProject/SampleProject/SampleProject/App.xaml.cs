using System;
using System.Diagnostics;
using Ninject;
using Ninject.Modules;
using SampleProject.Interfaces;
using SampleProject.Modules;
using SampleProject.Startup;
using SampleProject.ViewModels;
using SampleProject.Views;
using Xamarin.Forms;

namespace SampleProject
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
            AppLoader.Startup<LoginViewModel>(new LoginPage(), platformModules);
            MainPage = AppLoader.MainPage;
        }

        protected override void OnStart()
        {
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
