using HitechCraft.BL.CQRS.Command;
using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.WebAdmin.Mapper.Modification;
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

            this._kernel.Bind(typeof(ICommandHandler<NewsCreateCommand>)).To(typeof(NewsCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsRemoveCommand>)).To(typeof(NewsRemoveCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsUpdateCommand>)).To(typeof(NewsUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsViewsUpdateCommand>)).To(typeof(NewsViewsUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsImageRemoveCommand>)).To(typeof(NewsImageRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<ShopItemCreateCommand>)).To(typeof(ShopItemCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemRemoveCommand>)).To(typeof(ShopItemRemoveCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemUpdateCommand>)).To(typeof(ShopItemUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemBuyCommand>)).To(typeof(ShopItemBuyCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemCategoryRemoveCommand>)).To(typeof(ShopItemCategoryRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<ServerCreateCommand>)).To(typeof(ServerCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ServerUpdateCommand>)).To(typeof(ServerUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ServerRemoveCommand>)).To(typeof(ServerRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<ModificationCreateCommand>)).To(typeof(ModificationCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ModificationUpdateCommand>)).To(typeof(ModificationUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ModificationRemoveCommand>)).To(typeof(ModificationRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<SkinCreateCommand>)).To(typeof(SkinCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<SkinRemoveCommand>)).To(typeof(SkinRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<RuleCreateCommand>)).To(typeof(RuleCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<RuleRemoveCommand>)).To(typeof(RuleRemoveCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<RuleUpdateCommand>)).To(typeof(RuleUpdateCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<RulePointCreateCommand>)).To(typeof(RulePointCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<RulePointUpdateCommand>)).To(typeof(RulePointUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<RulePointRemoveCommand>)).To(typeof(RulePointRemoveCommandHandler));

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

            this._kernel.Bind(typeof(IProjector<NewsEditViewModel, NewsUpdateCommand>)).To(typeof(NewsEditViewModelToNewsUpdateCommandMapper));
            
            this._kernel.Bind(typeof(IProjector<ModificationEditViewModel, ModificationCreateCommand>)).To(typeof(ModificationEditViewModelToModificationCreateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ModificationEditViewModel, ModificationUpdateCommand>)).To(typeof(ModificationEditViewModelToModificationUpdateCommandMapper));

            this._kernel.Bind(typeof(IProjector<Modification, ModificationViewModel>)).To(typeof(ModificationToModificationViewModelMapper));
            this._kernel.Bind(typeof(IProjector<Modification, ModificationEditViewModel>)).To(typeof(ModificationToModificationEditViewModelMapper));

            this._kernel.Bind(typeof(IProjector<ShopItem, ShopItemEditViewModel>)).To(typeof(ShopItemToShopItemEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItem, ShopItemViewModel>)).To(typeof(ShopItemToShopItemViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryEditViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemCreateCommand>)).To(typeof(ShopItemEditViewModelToShopItemCreateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemUpdateCommand>)).To(typeof(ShopItemEditViewModelToShopItemUpdateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryEditViewModelToShopItemCategoryCreateCommandMapper));

            this._kernel.Bind(typeof(IProjector<Server, ServerViewModel>)).To(typeof(ServerToServerViewModelMapper));
            this._kernel.Bind(typeof(IProjector<Server, ServerDetailViewModel>)).To(typeof(ServerToServerDetailViewModelMapper));
            this._kernel.Bind(typeof(IProjector<Server, ServerEditViewModel>)).To(typeof(ServerToServerEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ServerEditViewModel, ServerCreateCommand>)).To(typeof(ServerEditViewModelToServerCreateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ServerEditViewModel, ServerUpdateCommand>)).To(typeof(ServerEditViewModelToServerUpdateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ServerCreateCommand, Server>)).To(typeof(ServerCreateCommandToServerMapper));

            this._kernel.Bind(typeof(IProjector<Skin, SkinViewModel>)).To(typeof(SkinToSkinViewModelMapper));
            this._kernel.Bind(typeof(IProjector<Skin, SkinEditViewModel>)).To(typeof(SkinToSkinEditViewModelMapper));

            this._kernel.Bind(typeof(IProjector<RulePoint, RulePointViewModel>)).To(typeof(RuleToRuleViewModelMapper));

            #endregion

            _kernel.Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));
        }
    }
}