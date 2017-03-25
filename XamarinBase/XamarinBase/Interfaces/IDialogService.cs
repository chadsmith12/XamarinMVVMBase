using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBase.Enumerations;

namespace XamarinBase.Interfaces
{
    /// <summary>
    /// Interface taht defines dialog boxes to show to the user.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Initializes the specified dialog page.
        /// </summary>
        /// <param name="dialogPage">The dialog page.</param>
        void Init(Page dialogPage);

        /// <summary>
        /// Shows a message box of <paramref name="dialogType"/> that displays information to the user.
        /// </summary>
        /// <param name="dialogType">Type of the dialog to show. See <see cref="DialogType"/> for valid types to show.</param>
        /// <param name="title">The title for the dialog box to show to the user.</param>
        /// <param name="message">The message to show the user.</param>
        /// <param name="buttonText">The text shown in the button in the dialog box.</param>
        /// <param name="hideCallback">An action that should be executed afte the dialog box is closed by the user.</param>
        /// <returns>Task to act off of.</returns>
        Task ShowMessage(DialogType dialogType, string title, string message, string buttonText, Action hideCallback);

        /// <summary>
        /// Shows a confirmation box to the user. Gives the user a choice to confirm or cancel.
        /// </summary>
        /// <param name="title">The title for the dialog box to show to the user.</param>
        /// <param name="message">The message to show the user.</param>
        /// <param name="confirmText">The text shown on the confirm button.</param>
        /// <param name="cancelText">The text shown on the cancel button.</param>
        /// <param name="hideCallBack">An action that should be executed after the confirmation box is closed. This will get passed in a boolean indicating if the user pressed
        /// confirm (true) or cancel (false).</param>
        /// <returns>Task to act off of that returns true or false if the user confirmed or cancled.</returns>
        Task<bool> ShowMessage(string title, string message, string confirmText, string cancelText, Action<bool> hideCallBack);
    }
}
