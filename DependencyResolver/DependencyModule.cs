using BLL.Interface.Services;
using BLL.Services;
using DAL.Interface.IRepositories;
using DAL.Repositories;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace DependencyResolver
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPictureService>().To<PictureService>();
            Bind<IPictureRepository>().To<PictureRepository>();
        }
    }
}