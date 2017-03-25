// Helpers/Settings.cs
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
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string DatabaseNameKey = "database_name";
    private static readonly string DatabaseNameValue = "Default.db3";

    #endregion


    public static string DatabaseName
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(DatabaseNameKey, DatabaseNameValue);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(DatabaseNameKey, value);
      }
    }

  }
}