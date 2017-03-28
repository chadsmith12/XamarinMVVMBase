using System;
using System.Diagnostics;
using Ninject;
using Ninject.Modules;
using SampleProject.Interfaces;
using SampleProject.Modules;
using SampleProject.ViewModels;
using SampleProject.Views;
using Xamarin.Forms;

namespace SampleProject
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
        /// <param name="platformModules">The platoform specific modules that need to be loaded into the kernal.</param>
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            try
            {
                var mainPage = new NavigationPage(new Views.LoginPage());
                Kernal = new StandardKernel();
                // Register all the modules with the kernal
                // We register the platform specific modules first because they have the bindings to the IDatabase which the ServiceModule will need.
                Kernal.Load(platformModules);
                Kernal.Load(new ServiceModule(), new ViewModelModule(), new NavigationModule(mainPage.Navigation));

                mainPage.BindingContext = Kernal.Get<LoginViewModel>();
                ((LoginViewModel) mainPage.BindingContext).DialogService = Kernal.Get<IDialogService>();
                ((LoginViewModel) mainPage.BindingContext).DialogService.Init(mainPage);

                MainPage = mainPage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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
