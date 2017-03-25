using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBase.ViewModels;

namespace XamarinBase
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
