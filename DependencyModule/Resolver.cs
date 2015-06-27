using BLL.Interface.Services;
using BLL.Services;
using DAL.Interface.IRepositories;
using DAL.Repositories;
using Ninject.Modules;


namespace DependencyModule
{
    public class Resolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IPictureService>().To<PictureService>();
            Bind<IPictureRepository>().To<PictureRepository>();
            Bind<IUserService>().To<UserService>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
