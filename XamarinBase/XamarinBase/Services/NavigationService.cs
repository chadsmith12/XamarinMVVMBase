using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using Xamarin.Forms;
using XamarinBase.Base;
using XamarinBase.Interfaces;

namespace XamarinBase.Services
{
    public class NavigationService : INavigationService
    {
        #region Private Fields
        // used internally to hold a mpaping vetween the view model (key) and the view (value)
        private readonly IDictionary<Type, Type> _viewModelMap = new Dictionary<Type, Type>();
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets a reference to the current INavigation instance in Xamarin.Forms
        /// </summary>
        /// <value>
        /// The xamarin navigation.
        /// </value>
        public INavigation XamarinNavigation { get; set; }

        /// <summary>
        /// Gets a value indicating whether we can go back in the navigation stack.
        /// We can go back on the stack when there is actually something on the stack.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoBack => XamarinNavigation.NavigationStack != null && XamarinNavigation.NavigationStack.Count > 0;
        #endregion

        #region Methods        
        /// <summary>
        /// Registers the view mapping.
        /// Registers a mapping from a ViewModel to a View
        /// </summary>
        /// <param name="viewModel">The type of view model.</param>
        /// <param name="view">The type of view.</param>
        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _viewModelMap.Add(viewModel, view);
        }

        /// <summary>
        /// Goes back to the previous page in the navigation stack.
        /// </summary>
        /// <returns>
        /// A task to act off of.
        /// </returns>
        public async Task GoBackAsync()
        {
            if (CanGoBack)
            {
                await XamarinNavigation.PopAsync(true);
            }

            OnCanGoBackChanged();
        }

        /// <summary>
        /// Navigates to the specified view model.
        /// Can pass in true to <paramref name="asModal" /> to navigate to it as a modal.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model. Must inherit from <see cref="BaseViewModel" /></typeparam>
        /// <param name="asModal">if set to <c>true</c> navigates to the view model as a modal.</param>
        /// <returns>
        /// A Task to act off of.
        /// </returns>
        public async Task NavigateToAsync<TViewModel>(bool asModal = false) where TViewModel : BaseViewModel
        {
            await NavigateToViewAsync(typeof(TViewModel), asModal);

            // get the view model for this view and initialize it if it hasn't been yet
            var viewModel = XamarinNavigation.NavigationStack.Last().BindingContext as BaseViewModel;
            if (viewModel != null)
            {
                await viewModel.Init();
            }
        }

        /// <summary>
        /// Navigates to the specified view model passing the specified parameter from <paramref name="parameter" />.
        /// Can pass in true to <paramref name="asModal" /> to navigate to it as a modal.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model. Must inherit from <see cref="BaseViewModel" /></typeparam>
        /// <typeparam name="TParameter">The type of the parameter that are passing in.</typeparam>
        /// <param name="parameter">The parameter we are passing in to the view model.</param>
        /// <param name="asModal">if set to <c>true</c> navigates to the view model as a modal.</param>
        /// <returns>
        /// A Task to act off of.
        /// </returns>
        public async Task NavigateToAsync<TViewModel, TParameter>(TParameter parameter, bool asModal = false) where TViewModel : BaseViewModel
        {
            await NavigateToViewAsync(typeof(TViewModel), asModal);

            // get the view model for this view and initialize it if it hasn't been yet
            var viewModel = XamarinNavigation.NavigationStack.Last().BindingContext as BaseViewModel<TParameter>;
            if (viewModel != null)
            {
                await viewModel.Init(parameter);
            }
        }

        /// <summary>
        /// Removes the last view from the navigation stack.
        /// </summary>
        /// <returns>
        /// A Task to act off of.
        /// </returns>
        public async Task RemoveLastViewAsync()
        {
            if (XamarinNavigation.NavigationStack.Count > 0)
            {
                await Task.Run(() =>
                {
                    var lastView = XamarinNavigation.NavigationStack[XamarinNavigation.NavigationStack.Count - 2];
                    XamarinNavigation.RemovePage(lastView);
                    OnCanGoBackChanged();
                });
            }
        }

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        /// <returns>
        /// A Task to act off of.
        /// </returns>
        public async Task ClearBackStackAsync()
        {
            if (XamarinNavigation.NavigationStack.Count <= 1)
            {
                return;
            }

            await Task.Run(() =>
            {
                for (var i = 0; i < XamarinNavigation.NavigationStack.Count - 1; ++i)
                {
                    XamarinNavigation.RemovePage(XamarinNavigation.NavigationStack[i]);
                }
            });
        }

        /// <summary>
        /// Navigates to the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        /// A task to act off of.
        /// </returns>
        /// <exception>uri - Invalid URI. URI Can not be NULL.
        ///     <cref>System.ArgumentNullException</cref>
        /// </exception>
        public async Task NavigateToUriAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri), "Invalid URI. URI Can not be NULL.");
            }

            await Task.Run(() =>
            {
                Device.OpenUri(uri);
            });
        }


        // internal helper to fire the event that we can go back or not.
        private void OnCanGoBackChanged()
        {
            var handler = CanGoBackChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(CanGoBack)));
        }

        // internal helper to navigate to a view that is mapped to a the specified view model type.
        private async Task NavigateToViewAsync(Type viewModelType, bool asModal = false)
        {
            Type viewType;
            if (!_viewModelMap.TryGetValue(viewModelType, out viewType))
            {
                throw new ArgumentException($"No view found in View Mapping for {viewModelType.FullName}.");
            }

            // try to find the empty constructor for this view and invoke it to initialize this page
            var constructor = viewType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(x => !x.GetParameters().Any());
            var view = constructor.Invoke(null) as Page;

            // get a new instance of the view model and automatically bind it to the views binding context
            var currentApp = (App) Application.Current;
            var viewModel = currentApp.Kernal.GetService(viewModelType);

            if (view == null)
            {
                throw new NullReferenceException($"Could not load view of type {viewType.FullName} registered to the {viewModelType.FullName}");
            }

            view.BindingContext = viewModel;

            if (asModal)
            {
                await XamarinNavigation.PushModalAsync(view, true);
            }
            else
            {
                await XamarinNavigation.PushAsync(view, true);
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler CanGoBackChanged;
        #endregion
    }
}
