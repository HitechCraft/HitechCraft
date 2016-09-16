using System;
using System.Web;
using System.Web.Mvc;
using HitechCraft.WebAdmin;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using global::Ninject;
using global::Ninject.Web.Common;
using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.Entity;
using HitechCraft.Ninjector.Dependences;
using HitechCraft.Projector.Impl;
using HitechCraft.WebAdmin.Mapper;
using HitechCraft.WebAdmin.Models;
using HitechCraft.WebAdmin.Ninject;
using Ninject.Extensions.Conventions;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace HitechCraft.WebAdmin
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new CommonModule(), new AdminModule());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}
