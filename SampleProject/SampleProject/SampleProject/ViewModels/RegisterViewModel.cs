using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using SampleProject.Base;
using SampleProject.Interfaces;
using SampleProject.Models;
using Xamarin.Forms;

namespace SampleProject.ViewModels
{
    [ImplementPropertyChanged]
    public class RegisterViewModel : BaseViewModel
    {
        private ICommand _registerCommand;

        public RegisterModel Model { get; set; }
        public RegisterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Model = new RegisterModel();
        }

        public override async Task Init()
        {
            await NavigationService.RemoveLastViewAsync();
        }

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (new Command(async () =>
                {
                    IsBusy = true;
                    await Task.Delay(2000);
                    IsBusy = false;
                    var goToLogin = await DialogService.ShowMessage("Thank You", "Registration Successful. Would you like to go Login?", "Yes", "No", null);
                    if (goToLogin)
                    {
                        await NavigationService.NavigateToAsync<LoginViewModel>();
                        await NavigationService.ClearBackStackAsync();
                    }
                }));
            }
        }
    }
}
