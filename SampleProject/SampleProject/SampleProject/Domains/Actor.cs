using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace SampleProject.Domains
{
    public class Actor : Base.BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey(typeof(Movie))]
        public int MovieId { get; set; }

        [ManyToOne()]
        public virtual Movie Movie { get; set; }
    }
}
