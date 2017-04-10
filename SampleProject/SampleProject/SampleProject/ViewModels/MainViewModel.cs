using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
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

        private readonly IMovieService _movieService;

        public MainViewModel(INavigationService navigationService, IMovieService movieService) : base(navigationService)
        {
            _movieService = movieService;
        }

        public override async Task Init()
        {

        }

        public int NumberMovies { get; set; } = 100;
        public string Message { get; set; }

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
                    await DialogService.ShowMessage(DialogType.Message, "Create Movie Successful!", $"Generated {NumberMovies} Movies in {stopWatch.Elapsed.Seconds} seconds", "Ok", null);
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
                    await DialogService.ShowMessage(DialogType.Message, "Delete Movie Successful!", $"Deleted {NumberMovies} Movies in {stopWatch.Elapsed.Seconds} seconds", "Ok", null);
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
