using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace XamarinBase.Interfaces
{
    /// <summary>
    /// An interface that any dataprovider can implement to bring a of working with entities in a database.
    /// This interface is always implemented using SQLite by default in the base project, though it is possible to implement this interface using a different data provider.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets all of the entities of type &lt;typeparamref name="TEntity"/&gt;.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <seealso cref="AsyncTableQuery{T}"/>
        /// <returns>An async table query that lets you preform and chain more async queries.
        /// </returns>
        AsyncTableQuery<TEntity> GetAllAsync<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Gets all of the entities of type &amp;lt;typeparamref name="TEntity"/&amp;gt; by the predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>An async table query that lets you preform and chain more async queries.</returns>
        AsyncTableQuery<TEntity> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;
            /// <summary>
        /// Finds all entities by the specified predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task to act off of that returns an IEnumerable of the entities.</returns>
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;

        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Task to act off of that returns the entity.</returns>
        Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IEntity;

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Task to act off of that returnst he id of the entity inserted.</returns>
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Task to act off of that returnst he id of the entity updated.</returns>
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Deletes the entity by it's identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Task to act off of that returns the id of the entity.</returns>
        Task<int> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;
    }
}
