using SampleProject.Enumerations;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace SampleProject.Domains
{
    public class Movie : Base.BaseEntity
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }
        public MovieRating MovieRating { get; set; }

        [OneToMany()]
        public List<Actor> Actors { get; set; }
    }
}
