using SampleProject.ViewModels;
using Xamarin.Forms;

namespace SampleProject
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel Vm => BindingContext as MainViewModel;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Vm != null)
            {
                await Vm.Init();
            }
        }
    }
}
