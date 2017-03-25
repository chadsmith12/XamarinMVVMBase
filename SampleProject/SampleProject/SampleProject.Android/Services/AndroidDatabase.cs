using System;
using System.IO;
using SampleProject.Interfaces;
using SQLite.Net;
using SQLite.Net.Async;

namespace XamarinBase.Droid.Services
{
    /// <summary>
    /// Implements the SQLIte Database Connection for Android
    /// </summary>
    public class AndroidDatabase : IDatabase
    {
        private SQLiteConnectionWithLock _connection;

        public SQLiteAsyncConnection DbConnect(string dbName)
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(folderPath, dbName);
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => _connection ?? (_connection = new SQLiteConnectionWithLock(platform, new SQLiteConnectionString(path, storeDateTimeAsTicks: false))));

            return new SQLiteAsyncConnection(connectionFactory);
        }
    }
}