using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using App.Repository;
using App.Repository.Repositories;
using App.Repository.UnitOfWorks;
using App.Service.Mapping;
using App.Service.Services;
using Autofac;

namespace App.API.Modules
{
    public class  RepoServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {   

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>))
                .InstancePerLifetimeScope();         

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();  

                

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly =  Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly,repoAssembly,serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly,repoAssembly,serviceAssembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();


        }
    }
}