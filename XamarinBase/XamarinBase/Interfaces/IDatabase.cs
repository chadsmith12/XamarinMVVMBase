using SQLite.Net.Async;

namespace XamarinBase.Interfaces
{
    /// <summary>
    /// Interface for all SQLite Connections
    /// When needed to create a new platform specific connection to SQLIte implement this interface to your class to get connection information.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Connects to the specified database.
        /// This provides a Async SQLite Connection
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <returns></returns>
        SQLiteAsyncConnection DbConnect(string dbName);
    }
}
