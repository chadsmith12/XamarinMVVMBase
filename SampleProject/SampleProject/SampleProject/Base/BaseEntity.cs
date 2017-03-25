using SampleProject.Interfaces;
using SQLite.Net.Attributes;

namespace SampleProject.Base
{
    public class BaseEntity : IEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
    }
}
