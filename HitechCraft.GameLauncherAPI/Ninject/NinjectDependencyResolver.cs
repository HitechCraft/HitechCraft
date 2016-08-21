using HitechCraft.GameLauncherAPI.Mapper;
using HitechCraft.GameLauncherAPI.Models;
using HitechCraft.WebApplication.Mapper;

namespace HitechCraft.GameLauncherAPI.Ninject
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Common.CQRS.Command;
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Models.Json.MinecraftLauncher;
    using Common.Projector;
    using Common.Repository;
    using Common.UnitOfWork;
    using DAL.Domain;
    using DAL.UnitOfWork;

    using global::Ninject;

    #endregion

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
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
            _kernel.Bind(typeof(IContainer)).To(typeof(BaseContainer));

            _kernel.Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));
            
            #region Command Bindings

            _kernel.Bind(typeof(ICommandHandler<>)).To(typeof(BaseCommandHandler<>));

            _kernel.Bind(typeof(ICommandHandler<PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<PlayerSessionCreateCommand>)).To(typeof(PlayerSessionCreateCommandHandler));

            #endregion

            _kernel.Bind(typeof(ICommandExecutor)).To(typeof(CommandExecutor));
        
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityListQueryHandler<,>));
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityQueryHandler<,>));

            #region Projector Ninjects

            _kernel.Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));
            
            _kernel.Bind(typeof(IProjector<PlayerSkin, PlayerSkinModel>)).To(typeof(PlayerSkinToPlayerSkinModelMapper));
            _kernel.Bind(typeof(IProjector<PlayerSession, PlayerSessionModel>)).To(typeof(PlayerSessionToPlayerSessionModelMapper));
            _kernel.Bind(typeof(IProjector<PlayerSessionModel, PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionModelToPlayerSessionUpdateCommandMapper));
            _kernel.Bind(typeof(IProjector<News, JsonLauncherNews>)).To(typeof(NewsToJsonLauncherNewsMapper));

            #endregion

            _kernel.Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));
        }
    }
}