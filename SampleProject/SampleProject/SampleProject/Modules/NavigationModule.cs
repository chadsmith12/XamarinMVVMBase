using Ninject.Modules;
using SampleProject.Interfaces;
using SampleProject.Services;
using SampleProject.ViewModels;
using Xamarin.Forms;

namespace SampleProject.Modules
{
    /// <summary>
    /// Module to bind the navigation mappings.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class NavigationModule : NinjectModule
    {
        #region Private Fields

        private readonly INavigation _formsNavigation;

        #endregion

        #region Constructors

        public NavigationModule(INavigation formsNavigation)
        {
            _formsNavigation = formsNavigation;
        }
        #endregion

        public override void Load()
        {
            // make the navigation service to register the view model to view mappings
            var navigationService = new NavigationService{XamarinNavigation = _formsNavigation};

            // Register the view mappings here
            // Example:
            // navigationService.RegisterViewMapping(typeof(MyViewModel), typeof(MyPage));
            navigationService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));

            // Bind the navigation service so it gets injected into the view models.
            // You only ever want one navigation service and not multiple navigation services laying around so we make a rule that this is done in the SingletonScope
            Bind<INavigationService>().ToMethod(x => navigationService).InSingletonScope();
        }
    }
}
