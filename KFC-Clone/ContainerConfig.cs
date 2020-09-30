using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KFC_Clone.Logging;
using KFC_Clone.Models.Repository;
using KFC_Clone.Lib;
using KFC_Clone.Models;
using SaltingAndHashing.Models;
using KFC_Clone.Models.DBModels;
using System.Web.Mvc;

namespace KFC_Clone
{
    public static class ContainerConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<Logging.Logging>().As<ILogging>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<SecurityService>().As<ISecurityService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<Repository<Order>>().As<IRepository<Order>>();
            builder.RegisterType<Repository<User>>().As<IRepository<User>>();
            builder.RegisterType<KFCDBEntities>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}