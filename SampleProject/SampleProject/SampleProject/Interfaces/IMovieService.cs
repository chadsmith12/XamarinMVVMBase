using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Domains;
using SampleProject.Enumerations;

namespace SampleProject.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll(Expression<Func<Movie, bool>> predicate); 
        Task<IEnumerable<Movie>> GetAllByRating(MovieRating rating);
        Task<Movie> GetById(int id);
        Task Save(Movie movie);
        Task Delete(int id);
    }
}
