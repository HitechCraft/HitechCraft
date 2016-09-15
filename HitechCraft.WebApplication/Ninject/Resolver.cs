using System.Web.Mvc;
using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.Ninjector.Dependences
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Core.Entity;
    using Core.Models.Json;
    using DAL.UnitOfWork;
    using WebApplication.Ninject.Current;
    using global::Ninject;

    #endregion

    public class Resolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public Resolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {
            
        }
    }
}