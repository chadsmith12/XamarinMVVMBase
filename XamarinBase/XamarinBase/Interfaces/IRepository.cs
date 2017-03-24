using System.Threading.Tasks;
using SQLite.Net.Async;

namespace XamarinBase.Interfaces
{
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
