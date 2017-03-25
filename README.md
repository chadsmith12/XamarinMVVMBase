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
  
 XamarinFormsBase is **not** a Framework. This is why this project is distributed as a base project to start your projects from instead of as a NuGet Package like others. You should use this project as a base and expand on it as you need to. Every project is different and has a different set of requirments. It is only here to provide a base to start from.

### Dependency Injection
Dependency Injection is provided by Ninject. Base Project provides four different Modules to be loaded into the Ninject Kernal:

  - **Navigation Module**: *Use this module to bind the Navigation Mappings. Will Bind the Navigation Service and Register the view mappings* 
  - **Service Module**: *Use this module to bind your core services and the Repository*
  - **ViewModel Module**: *Use this moodule to bind all of your view Models so the Navigation Service can find them automatically.*
  - **Platform Modules**: *Each Platform Specific Project defines a **Platform Module** that can be used for any services that are platform specific.*
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

### MVVM Pattern
While not advertised as a MVVM Framework the project heavily based around ViewModels so provides some helpers to work with ViewModels. 
All ViewModels should inherit from ``BaseViewModel`` which implements some common things for all ViewModels. All ViewModels have an IsBusy and IsRefreshing Bindale Properties, and also automatically injects the INavigationService.
When a ViewModel requires a parameter to be passed in (maybe for a Detail View) then you can just inherit your ViewModel from the generic ``BaseViewModel<TParameter>`` base class.
Because a big thing on MVVM is to keep your Views as clean as possible all ViewModels have an ``OnAppearing()`` and ``OnBackButtonPressed`` methods that you can override just like you would in your pages. Just have all your pages inherit from ``BasePage`` and these methods will automatically be called at the right times. 

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
### Dialog Service
To keep from MessagingCenter from being abused and keeping your Views as clean as possible, you can inject ``IDialogService`` into your view models to display dialog boxes to your users on error or to have them confirm an action. Using IDialogService is easy. Just inject it into your ViewModel and use it like so:
```csharp
// Displays an Error Message to the user
await _dialogService.ShowMessage(DialogType.Error, "Invalid Login", 
"Username or Password was invalid. Please Try Again.", "OK", ()=>
{
    // Custom Callback Action Here
});

// Display a confirmation box to the user
var result = await _dialogService.ShowMessage("Are you sure?", "Are you sure you want to continue?", "Yes", "Cancel", (isConfirmed)=>
{
     // Custom Callback Action on what to do based if the user confimred or not
});
```
