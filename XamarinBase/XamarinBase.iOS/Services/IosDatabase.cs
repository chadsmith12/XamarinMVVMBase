using System;
using System.IO;
using SQLite.Net;
using SQLite.Net.Async;
using XamarinBase.Interfaces;

namespace XamarinBase.iOS.Services
{
    /// <summary>
    /// Implements the SQLIte Database Connection for Android
    /// </summary>
    /// <seealso cref="XamarinBase.Interfaces.IDatabase" />
    public class IosDatabase : IDatabase
    {
        private SQLiteConnectionWithLock _connection;

        public SQLiteAsyncConnection DbConnect(string dbName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var librarypPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(librarypPath, dbName);
            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => _connection ?? (_connection = new SQLiteConnectionWithLock(platform, new SQLiteConnectionString(path, storeDateTimeAsTicks: false))));

            return new SQLiteAsyncConnection(connectionFactory);
        }
    }
}
