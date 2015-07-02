using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Interface.IRepositories;
using DAL.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;
using ORM;


namespace DependencyModule
{
    public class Resolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            Bind<DbContext>().To<AlbumDbEntities>().InRequestScope();

            Bind<IPictureService>().To<PictureService>();
            Bind<IPictureRepository>().To<PictureRepository>();
            Bind<IUserService>().To<UserService>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
