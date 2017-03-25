using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SampleProject.Interfaces;
using SQLite.Net.Async;
using Xamarin.Forms;

namespace SampleProject.Repository
{
    /// <summary>
    /// SQLite Generic Repository Class.
    /// Provides access to a sqlite database.
    /// To be good for memory on a mobile device, only one instance of this class should be made, though can be avoided if needed.
    /// </summary>
    /// <seealso>
    ///     <cref>XamarinBase.Interfaces.IRepository</cref>
    /// </seealso>
    public class Repository : IRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public Repository(string dbName)
        {
            var currentApp = (App) Application.Current;
            var databaseService = (IDatabase) currentApp.Kernal.GetService(typeof(IDatabase));
            _database = databaseService.DbConnect(dbName);

            // Define/Create any of the tables the database needs here
            // Example:
            // _database.CreateTableAsync<Entity>();
        }

        /// <summary>
        /// Gets all of the entities of type &lt;typeparamref name="TEntity"/&gt;.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// An async table query that lets you preform and chain more async queries.
        /// </returns>
        /// <seealso cref="AsyncTableQuery{T}" />
        public AsyncTableQuery<TEntity> GetAllAsync<TEntity>() where TEntity : class, IEntity
        {
            return  _database.Table<TEntity>();
        }

        /// <summary>
        /// Gets all of the entities of type &amp;lt;typeparamref name="TEntity"/&amp;gt; by the predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// An async table query that lets you preform and chain more async queries.
        /// </returns>
        public AsyncTableQuery<TEntity> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity
        {
            return _database.Table<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Finds all entities by the specified predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// Task to act off of that returns an IEnumerable of the entities.
        /// </returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity
        {
            var entities = await _database.Table<TEntity>().Where(predicate).ToListAsync();

            return entities;
        }

        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task to act off of that returns the entity.
        /// </returns>
        public async Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IEntity
        {
            return await _database.GetAsync<TEntity>(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Task to act off of that returnst he id of the entity inserted.
        /// </returns>
        public async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return await _database.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Task to act off of that returnst he id of the entity updated.
        /// </returns>
        public async Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return await _database.UpdateAsync(entity);
        }

        /// <summary>
        /// Deletes the entity by it's identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task to act off of that returns the id of the entity.
        /// </returns>
        public async Task<int> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
        {
            return await _database.DeleteAsync<TEntity>(id);
        }
    }
}
