using Ninject;
using Ninject.Modules;
using XamarinBase.Base;
using XamarinBase.Interfaces;
using XamarinBase.Modules;
using Xamarin.Forms;

namespace XamarinBase.Startup
{
    /// <summary>
    /// A class that loads/bootstraps out the startup of the application.
    /// Responsible for loading in all your modules and setting up the main page for first time use.
    /// </summary>
    public class AppLoader
    {
        #region Private Fields
        private readonly IKernel _kernel;
        private Page _mainPage;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="AppLoader"/> class.
        /// </summary>
        public AppLoader()
        {
            _kernel = new StandardKernel();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the kernal.
        /// </summary>
        /// <value>
        /// The kernal.
        /// </value>
        public IKernel Kernal { get { return _kernel; } }

        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <value>
        /// The main page.
        /// </value>
        public Page MainPage { get { return _mainPage; } }
        #endregion

        #region Methods
        /// <summary>
        /// Method to setup the bootstrap and startup the application.
        /// This will setup the main page you pass in and load in all the kernals needed.
        /// Use this to start up your app and keep your App.xaml.cs constructor clean.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model that will be used for startup with the main page.</typeparam>
        /// <param name="mainPage">The main page.</param>
        /// <param name="platformModules">The platform modules.</param>
        public void Startup<TViewModel>(Page mainPage, params INinjectModule[] platformModules) where TViewModel : BaseViewModel
        {
            _mainPage = new NavigationPage(mainPage);
            RegisterPlatformModules(platformModules);
            RegisterServiceModules();
            InitializeViewModels();
            RegisterNavigation();
            SetupMainPage<TViewModel>();

            // Any additional startup logic can be done here...
        }

        /// <summary>
        /// Initializes the view models.
        /// Use this method to initialize and register your view models with the app.
        /// </summary>
        private void InitializeViewModels()
        {
            _kernel.Load(new ViewModelModule());
        }

        /// <summary>
        /// Registers the platform modules.
        /// Use this method to register your platform specific modules.
        /// </summary>
        private void RegisterPlatformModules(params INinjectModule[] platforModules)
        {
            _kernel.Load(platforModules);
        }

        /// <summary>
        /// Registers the service modules.
        /// Use this method to register your service modules.
        /// </summary>
        private void RegisterServiceModules()
        {
            _kernel.Load(new ServiceModule());
        }

        /// <summary>
        /// Registers the navigation.
        /// Use this method to register your navigations services.
        /// </summary>
        private void RegisterNavigation()
        {
            _kernel.Load(new NavigationModule(_mainPage.Navigation));
        }

        /// <summary>
        /// Sets up the main page.
        /// Use this method to setup the main page for first use in your application.
        /// </summary>
        private void SetupMainPage<TViewModel>() where TViewModel : BaseViewModel
        {
            // Set the Binding Context for the main page for first time use.
            _mainPage.BindingContext = _kernel.Get<TViewModel>();

            // Register and initialize the dialog service for first time use.
            // We must do this because since this page was not navigated to we didn't get it called automatically.
            ((TViewModel)_mainPage.BindingContext).DialogService = _kernel.Get<IDialogService>();
            ((TViewModel)_mainPage.BindingContext).DialogService.Init(_mainPage);
        }
        #endregion
    }
}
