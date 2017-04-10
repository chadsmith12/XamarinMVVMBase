using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Domains;
using SampleProject.Enumerations;
using SampleProject.Interfaces;

namespace SampleProject.Services
{
    public class MovieService : IMovieService
    {
        private IRepository _movieRepo;

        public MovieService(IRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<IEnumerable<Movie>> GetAll(Expression<Func<Movie, bool>> predicate)
        {
            var movies = _movieRepo.GetAllAsync(predicate);

            return await movies.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllByRating(MovieRating rating)
        {
            var movies = _movieRepo.GetAllAsync<Movie>(movie => movie.MovieRating == rating);

            return await movies.ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _movieRepo.GetByIdAsync<Movie>(id);
        }

        public async Task Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                await _movieRepo.InsertAsync(movie);
            }
            else
            {
                await _movieRepo.UpdateAsync(movie);
            }
        }

        public async Task Delete(int id)
        {
            await _movieRepo.DeleteAsync<Movie>(id);
        }
    }
}
