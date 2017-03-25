using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleProject.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : ContentPage
    {
        private bool _initialView = true;

        public BasePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the style sheet used for this page.
        /// </summary>
        /// <value>
        /// The style sheet.
        /// </value>
        public string StyleSheet { get; set; }

        protected void LoadStyles()
        {
            // not the initial view already have the styles, move on
            if (!_initialView)
            {
                return;
            }

            _initialView = false;

            // not style sheet, just return.
            if (string.IsNullOrEmpty(StyleSheet)) return;

            try
            {
                var styleSheet = Activator.CreateInstance(Type.GetType(StyleSheet)) as VisualElement;

                // merge the resources in automatically
                if (styleSheet == null) return;

                foreach (var resource in styleSheet.Resources)
                {
                    Resources.Add(resource.Key, resource.Value);
                }
            }
            catch
            {
                // ToDo: Capture and Log this. Style Sheet could not be loaded.
            }
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// This will automatically get the binding context and pass it onto the ViewModel automatically.
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;

            bindingContext?.OnBackButtonPressed();

            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// This will automatically get the binding context and pass it onto the ViewModel automatically and laod the styles onto the page.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var bindingContext = BindingContext as BaseViewModel;

            bindingContext?.OnAppearing();

            LoadStyles();
        }
    }
}
