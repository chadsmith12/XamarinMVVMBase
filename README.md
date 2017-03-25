# XamarinFormsBase
XamarinFormsBase acts as a base project template to  start your **Xamarin.Forms** projects. 
This base project aims to give you a headstart to stop you from reinventing the wheel for some of the most common operations in most mobile projects using **Xamarin.Forms**.


XamarinFormsBase Will Provide the following:

  - MVVM Pattern
  - Base HTTP Web Service to derivce/create your own API Calls
  - Navigation Centered Around ViewModels
  - Constructor Based Dependency Injection
  - Storing Persistent Non Sensitive Data
  - Offline Storage
 
### Dependency Injection
Dependency Injection is provided by Ninject. Base Project provides four different Modules to be loaded into the Ninject Kernal:

  - **Navigation Module**: *Use this module to bind the Navigation Mappings. Will Bind the Navigation Service and Register the view mappings* 
  - **Service Module**: *Use this module to bind your core services and the Repository*
  - **ViewModel Module**: *Use this moodule to bind all of your view Models so the Navigation Service can find them automatically.*
  - **Platform Modules**: *Each Platform Specific Project defines a **Platform Module** that can be used for any services that are platform specific.*
  - 
 ### Repository
Base Project uses a Repository for Database Operations. This Repository fully supports the async pattern and uses SQLite Async as the Backend Provider. Each Platform must implement **IDatabase** their own specific way to provide a connection to SQLite.

Repository is binded as a Singleton. This is done to save memory. If you have some specific requirments this can be avoided. Repository can be automatically injected when needed. Example below shows repository being used in a service:

Example Usage:
```csharp
pubilc class MovieService : IMovieService
{
     private readonly IRepository _movieRepo;
     
     public MovieService(IRepository movieRepo)
     {
          // Repostory will automatically be injected in
          _movieRepo = movieRepo;
     }
     
     public async Task<IList<Movie>> GetAll()
     {
          // get only rated r movies
          var query = _movieRepo.GetAllAsync<Movie>.Where(movie => movie.Rating == Rating.R);
          // we can chain our queries
          query = query.OrderByDescending(movie => movie.ReleaseDate);
          // We are finished with our async query, await the result now
          return await query.ToListAsync();
     }
}
```

### ViewModels
The Project uses **Fody.PropertyChanged**. This Means that ViewModels can just use the ``[ImplementPropertyChanged]`` attributte on top of your ViewModels. This allows you to write properties in your ViewModels like so:
```
public Movie Movie {get; set;}
```
When compiled this will automatically get compiled to:
```csharp
 public event PropertyChangedEventHandler PropertyChanged;

    Movie movie;
    public Movie Movie
    {
        get { return movie; }
        set
        {
            if (value != movie)
            {
                movie = value;
                OnPropertyChanged("Movie");
            }
        }
    }
    
public virtual void OnPropertyChanged(string propertyName)
    {
        var propertyChanged = PropertyChanged;
        if (propertyChanged != null)
        {
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
```
*ToDo: Put more Information Here On View Models*
