using SQLite.Net.Attributes;
using XamarinBase.Interfaces;

namespace XamarinBase.Base
{
    public class BaseEntity : IEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
    }
}
