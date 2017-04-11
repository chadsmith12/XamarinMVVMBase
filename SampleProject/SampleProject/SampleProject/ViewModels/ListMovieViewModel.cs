using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SampleProject.Base;
using SampleProject.Domains;
using SampleProject.Interfaces;
using Xamarin.Forms;

namespace SampleProject.ViewModels
{
    public class ListMovieViewModel : BaseViewModel
    {
        private readonly IMovieService _movieService;
        private ICommand _loadMoreMoviesCommand;

        public ListMovieViewModel(INavigationService navigationService, IMovieService movieService) : base(navigationService)
        {
            _movieService = movieService;
            Movies = new ObservableCollection<Movie>();
        }

        public ObservableCollection<Movie> Movies { get; set; }
        public string Title { get; set; } = "0 Movies";

        public ICommand LoadMoreMovies
        {
            get
            {
                return _loadMoreMoviesCommand ?? (_loadMoreMoviesCommand = new Command(async () =>
                {
                    await LoadMovies();
                }));
            }
        }

        public override async Task Init()
        {
            await LoadMovies();
        }

        private async Task LoadMovies()
        {
            var movies = await _movieService.GetAll(Movies.Count, 50);
            foreach (var movie in movies)
            {
                Movies.Add(movie);
            }
            Title = $"{Movies.Count} Movies";
        }
    }
}
