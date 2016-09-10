using HitechCraft.BL.CQRS.Command;
using HitechCraft.DAL.Domain;
using HitechCraft.WebAdmin.Mapper.User;
using HitechCraft.WebAdmin.Models;
using HitechCraft.WebAdmin.Models.User;

namespace HitechCraft.WebAdmin.Ninject
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Common.CQRS.Command;
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Projector;
    using Common.Repository;
    using Common.UnitOfWork;
    using DAL.UnitOfWork;
    using Mapper;

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
            _kernel.Bind(typeof(ICommandHandler<PlayerInfoUpdateCommand>)).To(typeof(PlayerInfoUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<PlayerSkinCreateOrUpdateCommand>)).To(typeof(PlayerSkinCreateOrUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<PlayerSkinRemoveCommand>)).To(typeof(PlayerSkinRemoveCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<PlayerRegisterCreateCommand>)).To(typeof(PlayerRegisterCreateCommandHandler));

            #endregion

            _kernel.Bind(typeof(ICommandExecutor)).To(typeof(CommandExecutor));
        
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityListQueryHandler<,>));
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityQueryHandler<,>));
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(PlayerByLoginQueryHandler));

            #region Projector Ninjects

            _kernel.Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));
            _kernel.Bind(typeof(IProjector<Currency, PlayerInfoViewModel>)).To(typeof(CurrencyToPlayerInfoViewModelMapper));
            _kernel.Bind(typeof(IProjector<PlayerSkin, PlayerSkinViewModel>)).To(typeof(PlayerSkinToPlayerSkinViewModelMapper));
            this._kernel.Bind(typeof(IProjector<News, NewsViewModel>)).To(typeof(NewsToNewsViewModelMapper));
            this._kernel.Bind(typeof(IProjector<News, NewsEditViewModel>)).To(typeof(NewsToNewsEditViewModelMapper));

            #endregion

            _kernel.Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));
        }
    }
}