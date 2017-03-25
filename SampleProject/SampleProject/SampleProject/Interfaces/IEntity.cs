namespace SampleProject.Interfaces
{
    /// <summary>
    /// Base entity interface
    /// All entities must implement this and get an Id.
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}
