using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace XamarinBase.Navigation
{
    /// <summary>
    /// Interface gives you access to modify how you navigate to certain view models and adding new pages to the navigation stack.
    /// The NavigationService is View Model based.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Whether you can currently go back on the navigation stack.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Goes back to to the previous page in the navigation stack.
        /// </summary>
        Task GoBackAsync();

        /// <summary>
        /// Navigates to the page defined in the navigation mapping for the View Model specified.
        /// Will navigate as a modal if <paramref name="asModal"/> is true.
        /// </summary>
        /// <typeparam name="TViewModel">The View Model mapped.</typeparam>
        /// <param name="asModal">if set to <c>true</c> will navigate to the View Model as a modal.</param>
        Task NavigateToAsync<TViewModel>(bool asModal = false);

        /// <summary>
        /// <see cref="NavigateToAsync{TViewModel}"/>
        /// Pass a parameter in when navigating to a new page. The parameter will be pased to the View Model automatically.
        /// </summary>
        /// <typeparam name="TViewModel">The View Model mapped.</typeparam>
        /// <typeparam name="TParameter">The type of parameter you are passing into the view model.</typeparam>
        /// <param name="parameter">The parameter you are passing into the view model.</param>
        /// <param name="asModal">if set to <c>true</c> will navigate to the View Model as a modal.</param>
        Task NavigateToAsync<TViewModel, TParameter>(TParameter parameter, bool asModal = false);

        /// <summary>
        /// Removes the last view from the navigation stack.
        /// </summary>
        void RemoveLastView();

        /// <summary>
        /// Clears the current back stack for the navigation stack.
        /// </summary>
        void ClearBackStack();

        /// <summary>
        /// Provides a way to navigate to the specified url. 
        /// </summary>
        /// <param name="uri">Uri to navigate to.</param>
        Task NavigateToUriAsync(Uri uri);

        /// <summary>
        /// Event that is fired when <see cref="CanGoBack"/> changes values.
        /// </summary>
        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
