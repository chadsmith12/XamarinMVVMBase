using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Xamarin.Forms;

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

        public App()
        {
            InitializeComponent();

            MainPage = new XamarinBase.MainPage();
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
