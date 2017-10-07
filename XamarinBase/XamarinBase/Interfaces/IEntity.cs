namespace XamarinBase.Interfaces
{
    /// <summary>
    /// Base entity interface
    /// All entities must implement this and get an Id.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The primary key identifier for an entity implementing this interface.
        /// </summary>
        int Id { get; set; }
    }
}
