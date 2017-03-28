using Ninject;
using Ninject.Modules;
using Xamarin.Forms;
using XamarinBase.Interfaces;
using XamarinBase.Modules;
using XamarinBase.ViewModels;

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
        /// <param name="platformModules">The platoform specific modules that need to be loaded into the kernal.</param>
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            // we must put some page, even if the page is nothing, for iOS
            var mainPage = new NavigationPage(new MainPage());
            Kernal = new StandardKernel();
            // Register all the modules with the kernal
            // We register the platform specific modules first because they have the bindings to the IDatabase which the ServiceModule will need.
            Kernal.Load(platformModules);
            Kernal.Load(new ServiceModule(), new ViewModelModule(), new NavigationModule(mainPage.Navigation));

            mainPage.BindingContext = Kernal.Get<MainViewModel>();
            ((MainViewModel)mainPage.BindingContext).DialogService = Kernal.Get<IDialogService>();
            ((MainViewModel)mainPage.BindingContext).DialogService.Init(mainPage);

            MainPage = mainPage;
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
