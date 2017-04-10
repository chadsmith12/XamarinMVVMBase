using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using SampleProject.Base;
using SampleProject.Enumerations;
using SampleProject.Interfaces;
using SampleProject.Models;
using Xamarin.Forms;

namespace SampleProject.ViewModels
{
    [ImplementPropertyChanged]
    public class LoginViewModel : BaseViewModel
    {
        private ICommand _registerCommand;
        private ICommand _loginCommand;

        public LoginModel Model { get; set; }

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
            Model = new LoginModel();
        }

        public override async Task Init()
        {
            // Init the View Model

        }

        #region Commands

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new Command(async () =>
                {
                    await NavigationService.NavigateToAsync<RegisterViewModel>();
                }));
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(async () =>
                {
                    IsBusy = true;
                    // This should actually call out to an actual authentication api/service to get the api token. 
                    // But for demo purposes just allow anyone to login if they enter demo:demo
                    if (Model.Email == "demo@demo.com" && Model.Password == "demo")
                    {
                        // simulate a request to show the busy indicator
                        await Task.Delay(2000);
                        IsBusy = false;
                        await NavigationService.NavigateToAsync<MainViewModel>();
                        await NavigationService.ClearBackStackAsync();
                    }
                    else
                    {
                        IsBusy = false;
                        await DialogService.ShowMessage(DialogType.Error, "Invalid Login", "The Email or Password was invalid. Please Try again", "Ok", null);
                    }
                }));
            }
        }
        #endregion
    }
}
