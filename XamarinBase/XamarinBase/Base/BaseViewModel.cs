using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using XamarinBase.Annotations;

namespace XamarinBase.Base
{
    [ImplementPropertyChanged]
    public abstract class BaseViewModel
    {
        #region Private Fields

        #endregion

        #region Constructors
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
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the video model without using a constructor.
        /// This is useful and used when a ViewModel just needs to be refreshed.
        /// </summary>
        /// <returns>Task to act off of.</returns>
        public abstract Task Init();

        #endregion
    }

    [ImplementPropertyChanged]
    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        #region Constructors

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
