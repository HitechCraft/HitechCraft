using HitechCraft.Core.Models.Json;
using HitechCraft.GameLauncherAPI.Mapper;
using HitechCraft.GameLauncherAPI.Models;
using HitechCraft.WebApplication.Mapper;

namespace HitechCraft.GameLauncherAPI.Ninject
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using HitechCraft.BL.CQRS.Command;
    using HitechCraft.Core.Entity;
    using HitechCraft.Projector.Impl;
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
            new Ninjector.Dependences.NinjectDependencyResolver(_kernel).AddBindings();

            #region Projector Ninjects

            _kernel.Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));

            _kernel.Bind(typeof(IProjector<PlayerSkin, PlayerSkinModel>)).To(typeof(PlayerSkinToPlayerSkinModelMapper));
            _kernel.Bind(typeof(IProjector<PlayerSession, PlayerSessionModel>)).To(typeof(PlayerSessionToPlayerSessionModelMapper));
            _kernel.Bind(typeof(IProjector<PlayerSession, JsonSessionData>)).To(typeof(PlayerSessionToJsonSessionDataMapper));
            _kernel.Bind(typeof(IProjector<PlayerSessionModel, PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionModelToPlayerSessionUpdateCommandMapper));
            _kernel.Bind(typeof(IProjector<News, JsonLauncherNews>)).To(typeof(NewsToJsonLauncherNewsMapper));

            #endregion
        }
    }
}