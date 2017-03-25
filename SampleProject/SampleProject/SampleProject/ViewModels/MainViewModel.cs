using System.Threading.Tasks;
using SampleProject.Base;
using SampleProject.Interfaces;

namespace SampleProject.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public override async Task Init()
        {
            // Init View Model Here
        }
    }
}
