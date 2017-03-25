using System.Threading.Tasks;
using PropertyChanged;
using XamarinBase.Interfaces;

namespace XamarinBase.Base
{
    /// <summary>
    /// Provides a common base class that all ViewModels should inherit from to implement.
    /// Provides common functionality that all ViewModels need or will use.
    /// </summary>
    [ImplementPropertyChanged]
    public abstract class BaseViewModel
    {
        #region Private Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        protected BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets a value indicating whether the view model is currently busy.
        /// Can use this to show a busy indicator to the user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the view model is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the view model is refreshing.
        /// Can use this on list views when swipe to refresh is being implemented.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is refreshing; otherwise, <c>false</c>.
        /// </value>
        public bool IsRefreshing { get; set; }

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        /// <value>
        /// The navigation service.
        /// </value>
        protected INavigationService NavigationService { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the video model without using a constructor.
        /// This is useful and used when a ViewModel just needs to be refreshed.
        /// </summary>
        /// <returns>Task to act off of.</returns>
        public abstract Task Init();

        /// <summary>
        /// Called when a view is appearing.
        /// Override this method in your ViewModels if you need something happen when the view is appearing or need more things to happen after Init.
        /// </summary>
        public virtual void OnAppearing()
        {
            
        }

        /// <summary>
        /// Called when the back button is pressed.
        /// Override this method when you need to know that the back button was pressed from the view.
        /// </summary>
        public virtual void OnBackButtonPressed()
        {
            
        }

        #endregion
    }

    /// <summary>
    /// Provides a common base class that all ViewMOdels should inherit from to implement.
    /// This takes in a generic when you have a ViewModel that has to have a parameter passed into it. 
    /// This could be from navigating from a ListView to an Item Detail you need to pass in the item we are viewing in the view model.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter you're passing into the ViewModel.</typeparam>
    [ImplementPropertyChanged]
    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel{TParameter}"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        protected BaseViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initializes the video model without using a constructor.
        /// This is useful and used when a ViewModel just needs to be refreshed.
        /// </summary>
        /// <returns>
        /// Task to act off of.
        /// </returns>
        public override async Task Init()
        {
            await Init(default(TParameter));
        }

        /// <summary>
        /// Initializes the video model without using a constructor using <paramref name="parameter"/>.
        /// This is useful and used when a ViewModel just needs to be refreshed.
        /// </summary>
        /// <param name="parameter">The parameter we need when initializing the view model.</param>
        /// <returns>Task to act off of.</returns>
        public abstract Task Init(TParameter parameter);
        #endregion
    }
}
