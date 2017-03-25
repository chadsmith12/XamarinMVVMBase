using Ninject.Modules;
using SampleProject.ViewModels;

namespace SampleProject.Modules
{
    /// <summary>
    /// Loads the view models into the kernal
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf();
        }
    }
}
