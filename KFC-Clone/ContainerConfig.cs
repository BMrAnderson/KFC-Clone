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
using System.Reflection;
using System.Diagnostics;

namespace KFC_Clone
{
    public static class ContainerConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var types = GetDependentTypes();

            for (int c = 0; c < types.Length/2; c++)
                builder.RegisterType(types[c, 0]).As(types[c, 1]);
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }

        private static Type[,] GetDependentTypes()
        {
            return new Type[,]
            {            
                {typeof(Logging.Logging),   typeof(Logging.ILogging)},
                {typeof(AccountService),    typeof(IAccountService)},
                {typeof(OrderService),      typeof(IOrderService)},
                {typeof(SecurityService),   typeof(ISecurityService)},
                {typeof(UserService),       typeof(IUserService)},
                {typeof(Repository<Order>), typeof(IRepository<Order>)},
                {typeof(Repository<User>),  typeof(IRepository<User>)},
            }; 
        }
    }
}