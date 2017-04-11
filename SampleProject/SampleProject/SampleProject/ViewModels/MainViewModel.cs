using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SampleProject.Interfaces;
using System.Windows.Input;
using SampleProject.Domains;
using SampleProject.Enumerations;
using Xamarin.Forms;

namespace SampleProject.ViewModels
{
    public class MainViewModel : Base.BaseViewModel
    {
        private ICommand _createMoviesCommand;
        private ICommand _deleteMoviesCommand;
        private ICommand _viewMoviesCommand;

        private readonly IMovieService _movieService;

        public MainViewModel(INavigationService navigationService, IMovieService movieService) : base(navigationService)
        {
            _movieService = movieService;
        }

        public override async Task Init()
        {
            HasMovies = await _movieService.HasMovies();
        }

        public int NumberMovies { get; set; } = 100;
        public string Message { get; set; }
        public bool HasMovies { get; set; }

        public ICommand CreateMoviesCommand
        {
            get
            {
                return _createMoviesCommand ?? (_createMoviesCommand = new Command(async () =>
                {
                    Message = "Generating Movies...";
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    IsBusy = true;
                    var movies = GenerateMovies(NumberMovies);
                    foreach (var movie in movies)
                    {
                        await _movieService.Save(movie);
                    }
                    stopWatch.Stop();
                    IsBusy = false;
                    HasMovies = true;
                }));
            }
        }

        public ICommand DeleteMoviesCommand
        {
            get
            {
                return _deleteMoviesCommand ?? (_deleteMoviesCommand = new Command(async () =>
                {
                    Message = "Deleting Movies...";
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    IsBusy = true;
                    var allMovies = await _movieService.GetAll(movie => movie.Id >= 0);
                    foreach (var movie in allMovies)
                    {
                        await _movieService.Delete(movie.Id);
                    }
                    stopWatch.Stop();
                    IsBusy = false;
                    HasMovies = await _movieService.HasMovies();
                }));
            }
        }

        public ICommand ViewMoviesCommand
        {
            get
            {
                return _viewMoviesCommand ?? (_viewMoviesCommand = new Command(async () =>
                {
                    await NavigationService.NavigateToAsync<ListMovieViewModel>();
                }));
            }
        }

        private IEnumerable<Movie> GenerateMovies(int numberToGenerate)
        {
            var movieList = new List<Movie>();

            for (var i = 0; i < numberToGenerate; ++i)
            {
                movieList.Add(new Movie
                {
                    Title = $"Movie {i}",
                    Director = $"Director {i}",
                    Plot = $"Plot {i}",
                    MovieRating = (MovieRating)(i % 3)
                });
            }

            return movieList;
        }
    }
}
