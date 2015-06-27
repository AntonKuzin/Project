using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using Ninject.Modules;

namespace test
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IPictureRepository>().To<PictureRepository>();
        }
    }
}