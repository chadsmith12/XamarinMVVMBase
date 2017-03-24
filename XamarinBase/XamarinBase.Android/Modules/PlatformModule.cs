using Ninject.Modules;

namespace XamarinBase.Droid.Modules
{
    /// <summary>
    /// Binds all the Android Platform Specific Modules into the kernal.
    /// This gets passed onto the app on launch and Forms handles loading it into the kernal.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class PlatformModule : NinjectModule
    {
        public override void Load()
        {
            // Bind Here
            // All services use the rule InSingletonScope so we only ever have one instance of that service hanging around.
            // IF it is ever needed that you have to have a brand new instance, this rule can be removed
            // Example: 
            // Bind<IService>().To<PlatformService>.InSingletonScope()
        }
    }
}