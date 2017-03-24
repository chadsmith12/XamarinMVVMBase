using Ninject.Modules;
using XamarinBase.Interfaces;

namespace XamarinBase.Modules
{
    /// <summary>
    /// Module to bind all services.
    /// Use this module to bind all the core services that need to be injected in.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            // Bind Core Services
            // Services can be bound here using method binding so we give it a callback it should be bound to
            // Example: 
            // var dataService = new DataService(new Uri("www.example.com"));
            // Bind<IDataService>().ToMethod(x => dataService)
            // OR: Bind<IExampleService>().To<ExampleService>()

            // Bind Repository
            // Respository is singleton for memory reasons on mobile database
            // You can avoid singleton if needed.
            var repository = new Repository.Repository("Default.db3");
            Bind<IRepository>().ToMethod(x => repository).InSingletonScope();
        }
    }
}
