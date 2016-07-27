using HitechCraft.Common.Models.Json.MinecraftLauncher;

namespace HitechCraft.WebApplication.Ninject
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
    using Common.Projector;
    using Common.Repository;
    using Common.UnitOfWork;
    using Mapper;
    using DAL.UnitOfWork;
    
    using DAL.Domain;
    using Models;
    using Current;

    using global::Ninject;

    #endregion

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this._kernel = kernel;
            this.AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return this._kernel.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {
            this._kernel.Bind(typeof(IContainer)).To(typeof(BaseContainer));

            this._kernel.Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));

            this._kernel.Bind(typeof(ICurrentUser)).To(typeof(CurrentUser));

            this._kernel.Bind(typeof(ICommandHandler<>)).To(typeof(BaseCommandHandler<>));
            this._kernel.Bind(typeof(ICommandHandler<PlayerRegisterCreateCommand>)).To(typeof(PlayerRegisterCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<PlayerSkinCreateOrUpdateCommand>)).To(typeof(PlayerSkinCreateOrUpdateCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<NewsCreateCommand>)).To(typeof(NewsCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsRemoveCommand>)).To(typeof(NewsRemoveCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsUpdateCommand>)).To(typeof(NewsUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsViewsUpdateCommand>)).To(typeof(NewsViewsUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<NewsImageRemoveCommand>)).To(typeof(NewsImageRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<CommentCreateCommand>)).To(typeof(CommentCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<CommentUpdateCommand>)).To(typeof(CommentUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<CommentRemoveCommand>)).To(typeof(CommentRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<ServerCreateCommand>)).To(typeof(ServerCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ServerUpdateCommand>)).To(typeof(ServerUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ServerRemoveCommand>)).To(typeof(ServerRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<ModificationCreateCommand>)).To(typeof(ModificationCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ModificationUpdateCommand>)).To(typeof(ModificationUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ModificationRemoveCommand>)).To(typeof(ModificationRemoveCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<IKTransactionCreateCommand>)).To(typeof(IKTransactionCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<IKTransactionUpdateCommand>)).To(typeof(IKTransactionUpdateCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<CurrencyCreateCommand>)).To(typeof(CurrencyCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<CurrencyUpdateCommand>)).To(typeof(CurrencyUpdateCommandHandler));
            
            this._kernel.Bind(typeof(ICommandHandler<PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<PlayerSessionCreateCommand>)).To(typeof(PlayerSessionCreateCommandHandler));

            this._kernel.Bind(typeof(ICommandHandler<ShopItemCreateCommand>)).To(typeof(ShopItemCreateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemRemoveCommand>)).To(typeof(ShopItemRemoveCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemUpdateCommand>)).To(typeof(ShopItemUpdateCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemBuyCommand>)).To(typeof(ShopItemBuyCommandHandler));
            this._kernel.Bind(typeof(ICommandHandler<ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryCreateCommandHandler));

            this._kernel.Bind(typeof(ICommandExecutor)).To(typeof(CommandExecutor));
        
            this._kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityListQueryHandler<,>));
            this._kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityQueryHandler<,>));
            this._kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(PlayerByLoginQueryHandler));

            #region Projector Ninjects

            this._kernel.Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));

            this._kernel.Bind(typeof(IProjector<Ban, BanViewModel>)).To(typeof(BanToBanViewModelMapper));

            this._kernel.Bind(typeof(IProjector<Server, ServerViewModel>)).To(typeof(ServerToServerViewModelMapper));
            this._kernel.Bind(typeof(IProjector<Server, ServerEditViewModel>)).To(typeof(ServerToServerEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ServerEditViewModel, ServerCreateCommand>)).To(typeof(ServerEditViewModelToServerCreateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ServerEditViewModel, ServerUpdateCommand>)).To(typeof(ServerEditViewModelToServerUpdateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ServerCreateCommand, Server>)).To(typeof(IKTransactionToIKTransactionViewModelMapper));

            this._kernel.Bind(typeof(IProjector<Modification, ServerModificationViewModel>)).To(typeof(ServerToServerViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ModificationEditViewModel, ModificationCreateCommand>)).To(typeof(ModificationEditViewModelToModificationCreateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ModificationEditViewModel, ModificationUpdateCommand>)).To(typeof(ModificationEditViewModelToModificationUpdateCommandMapper));

            this._kernel.Bind(typeof(IProjector<Modification, ModificationViewModel>)).To(typeof(ModificationToModificationViewModelMapper));
            this._kernel.Bind(typeof(IProjector<Modification, ModificationEditViewModel>)).To(typeof(ModificationToModificationEditViewModelMapper));

            this._kernel.Bind(typeof(IProjector<News, NewsViewModel>)).To(typeof(NewsToNewsViewModelMapper));
            this._kernel.Bind(typeof(IProjector<News, NewsEditViewModel>)).To(typeof(NewsToNewsEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<NewsEditViewModel, NewsUpdateCommand>)).To(typeof(NewsEditViewModelToNewsUpdateCommandMapper));

            this._kernel.Bind(typeof(IProjector<Comment, CommentViewModel>)).To(typeof(CommentToCommentViewModelMapper));
            this._kernel.Bind(typeof(IProjector<Comment, CommentEditViewModel>)).To(typeof(CommentToCommentEditViewModelMapper));

            this._kernel.Bind(typeof(IProjector<PlayerSkin, PlayerSkinViewModel>)).To(typeof(PlayerSkinToPlayerSkinViewModelMapper));
            
            this._kernel.Bind(typeof(IProjector<Player, PlayerProfileViewModel>)).To(typeof(PlayerToPlayerProfileViewModelMapper));
            this._kernel.Bind(typeof(IProjector<PlayerSession, PlayerSessionEditViewModel>)).To(typeof(PlayerSessionToPlayerSessionEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<PlayerSession, JsonSessionData>)).To(typeof(PlayerSessionToJsonSessionDataMapper));
            this._kernel.Bind(typeof(IProjector<PlayerSessionEditViewModel, PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionEditViewModelToPlayerSessionUpdateCommandMapper));

            this._kernel.Bind(typeof(IProjector<IKTransaction, IKTransactionViewModel>)).To(typeof(IKTransactionToIKTransactionViewModelMapper));
            
            this._kernel.Bind(typeof(IProjector<ShopItem, ShopItemEditViewModel>)).To(typeof(ShopItemToShopItemEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItem, ShopItemViewModel>)).To(typeof(ShopItemToShopItemViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryEditViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryEditViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryViewModelMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemCreateCommand>)).To(typeof(ShopItemEditViewModelToShopItemCreateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemUpdateCommand>)).To(typeof(ShopItemEditViewModelToShopItemUpdateCommandMapper));
            this._kernel.Bind(typeof(IProjector<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryEditViewModelToShopItemCategoryCreateCommandMapper));

            #endregion

            this._kernel.Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));
        }
    }
}