using System;
using System.ComponentModel;
using System.Threading.Tasks;
using SampleProject.Base;

namespace SampleProject.Interfaces
{
    /// <summary>
    /// Interface to define for all navigations.
    /// This interface gives us access to modify how we navgiate to certain view models and adding new pages to the navigation stack.
    /// Navigation is View Model based.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Gets a value indicating whether we can go back in the navigation stack.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        bool CanGoBack { get; }

        /// <summary>
        /// Goes back to the previous page in the navigation stack.
        /// </summary>
        /// <returns>A task to act off of.</returns>
        Task GoBackAsync();

        /// <summary>
        /// Navigates to the specified view model.
        /// Can pass in true to <paramref name="asModal"/> to navigate to it as a modal.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model. Must inherit from <see cref="BaseViewModel"/></typeparam>
        /// <param name="asModal">if set to <c>true</c> navigates to the view model as a modal.</param>
        /// <returns>A Task to act off of.</returns>
        Task NavigateToAsync<TViewModel>(bool asModal = false) where TViewModel : BaseViewModel;

        /// <summary>
        /// Navigates to the specified view model passing the specified parameter from <paramref name="parameter"/>.
        /// Can pass in true to <paramref name="asModal"/> to navigate to it as a modal.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model. Must inherit from <see cref="BaseViewModel"/></typeparam>
        /// <typeparam name="TParameter">The type of the parameter that are passing in.</typeparam>
        /// <param name="parameter">The parameter we are passing in to the view model.</param>
        /// <param name="asModal">if set to <c>true</c> navigates to the view model as a modal.</param>
        /// <returns>A Task to act off of.</returns>
        Task NavigateToAsync<TViewModel, TParameter>(TParameter parameter, bool asModal = false) where TViewModel : BaseViewModel;

        /// <summary>
        /// Removes the last view from the navigation stack.
        /// </summary>
        /// <returns>A Task to act off of.</returns>
        Task RemoveLastViewAsync();

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        /// <returns>A Task to act off of.</returns>
        Task ClearBackStackAsync();

        /// <summary>
        /// Navigates to the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>A task to act off of.</returns>
        Task NavigateToUriAsync(Uri uri);

        /// <summary>
        /// Occurs when [can go back changed].
        /// </summary>
        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
