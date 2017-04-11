using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinBase.Helpers
{
      /// <summary>
      /// This is the Settings static class that can be used in your Core solution or in any
      /// of your client applications. All settings are laid out the same exact way with getters
      /// and setters. 
      /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        #region Setting Constants
        // Database Setting Constants
        private const string DatabaseNameKey = "database_name";
        private static readonly string DatabaseNameValue = "Default.db3";

        // Constant to define how many items to grab for each pagination in an InfiteListView
        private const string PageKey = "page_key";
        private static readonly int PageValue = 50;
        #endregion

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public static string DatabaseName
        {
            get { return AppSettings.GetValueOrDefault(DatabaseNameKey, DatabaseNameValue); }
            set { AppSettings.AddOrUpdateValue(DatabaseNameKey, value); }
        }

        /// <summary>
        /// Gets or sets the number of items to grab when using pagination.
        /// </summary>
        /// <value>
        /// The pagiantion value.
        /// </value>
        public static int Page
        {
            get { return AppSettings.GetValueOrDefault(PageKey, PageValue); }
            set { AppSettings.AddOrUpdateValue(PageKey, value); }
        }
    }
}