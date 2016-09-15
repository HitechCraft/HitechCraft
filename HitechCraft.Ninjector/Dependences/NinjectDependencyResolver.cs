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

            _kernel.Bind(typeof(ICurrentUser)).To(typeof(CurrentUser));

            #region Command Bindings

            _kernel.Bind(typeof(ICommandHandler<>)).To(typeof(BaseCommandHandler<>));
            _kernel.Bind(typeof(ICommandHandler<PlayerRegisterCreateCommand>)).To(typeof(PlayerRegisterCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<PlayerSkinCreateOrUpdateCommand>)).To(typeof(PlayerSkinCreateOrUpdateCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<NewsCreateCommand>)).To(typeof(NewsCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<NewsRemoveCommand>)).To(typeof(NewsRemoveCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<NewsUpdateCommand>)).To(typeof(NewsUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<NewsViewsUpdateCommand>)).To(typeof(NewsViewsUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<NewsImageRemoveCommand>)).To(typeof(NewsImageRemoveCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<CommentCreateCommand>)).To(typeof(CommentCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<CommentUpdateCommand>)).To(typeof(CommentUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<CommentRemoveCommand>)).To(typeof(CommentRemoveCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<ServerCreateCommand>)).To(typeof(ServerCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ServerUpdateCommand>)).To(typeof(ServerUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ServerRemoveCommand>)).To(typeof(ServerRemoveCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<ModificationCreateCommand>)).To(typeof(ModificationCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ModificationUpdateCommand>)).To(typeof(ModificationUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ModificationRemoveCommand>)).To(typeof(ModificationRemoveCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<IKTransactionCreateCommand>)).To(typeof(IKTransactionCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<IKTransactionUpdateCommand>)).To(typeof(IKTransactionUpdateCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<CurrencyCreateCommand>)).To(typeof(CurrencyCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<CurrencyUpdateCommand>)).To(typeof(CurrencyUpdateCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<PlayerSessionCreateCommand>)).To(typeof(PlayerSessionCreateCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<ShopItemCreateCommand>)).To(typeof(ShopItemCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ShopItemRemoveCommand>)).To(typeof(ShopItemRemoveCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ShopItemUpdateCommand>)).To(typeof(ShopItemUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ShopItemBuyCommand>)).To(typeof(ShopItemBuyCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryCreateCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<ShopItemAddRandomCommand>)).To(typeof(ShopItemAddRandomCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<DonateGroupIEBuyCommand>)).To(typeof(DonateGroupIEBuyCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<AuthLogCreateCommand>)).To(typeof(AuthLogCreateCommandHandler));
            
            _kernel.Bind(typeof(ICommandHandler<RuleCreateCommand>)).To(typeof(RuleCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<RuleRemoveCommand>)).To(typeof(RuleRemoveCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<RuleUpdateCommand>)).To(typeof(RuleUpdateCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<RulePointCreateCommand>)).To(typeof(RulePointCreateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<RulePointUpdateCommand>)).To(typeof(RulePointUpdateCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<RulePointRemoveCommand>)).To(typeof(RulePointRemoveCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<SkinCreateCommand>)).To(typeof(SkinCreateCommandHandler));

            _kernel.Bind(typeof(ICommandHandler<SkinInstallCommand>)).To(typeof(SkinInstallCommandHandler));
            
            _kernel.Bind(typeof(ICommandHandler<PMInboxReadCommand>)).To(typeof(PMInboxReadCommandHandler));
            _kernel.Bind(typeof(ICommandHandler<PMInboxRemoveCommand>)).To(typeof(PMInboxRemoveCommandHandler));

            #endregion

            _kernel.Bind(typeof(ICommandExecutor)).To(typeof(CommandExecutor));
        
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityListQueryHandler<,>));
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityQueryHandler<,>));
            _kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(PlayerByLoginQueryHandler));

            #region Projector Ninjects

            _kernel.Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));

            _kernel.Bind(typeof(IProjector<Ban, BanViewModel>)).To(typeof(BanToBanViewModelMapper));

            _kernel.Bind(typeof(IProjector<Server, ServerViewModel>)).To(typeof(ServerToServerViewModelMapper));
            _kernel.Bind(typeof(IProjector<Server, ServerDetailViewModel>)).To(typeof(ServerToServerDetailViewModelMapper));
            _kernel.Bind(typeof(IProjector<Server, ServerEditViewModel>)).To(typeof(ServerToServerEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<ServerEditViewModel, ServerCreateCommand>)).To(typeof(ServerEditViewModelToServerCreateCommandMapper));
            _kernel.Bind(typeof(IProjector<ServerEditViewModel, ServerUpdateCommand>)).To(typeof(ServerEditViewModelToServerUpdateCommandMapper));
            _kernel.Bind(typeof(IProjector<ServerCreateCommand, Server>)).To(typeof(ServerCreateCommandToServerMapper));

            _kernel.Bind(typeof(IProjector<Modification, ServerModificationViewModel>)).To(typeof(ServerToServerViewModelMapper));
            _kernel.Bind(typeof(IProjector<ModificationEditViewModel, ModificationCreateCommand>)).To(typeof(ModificationEditViewModelToModificationCreateCommandMapper));
            _kernel.Bind(typeof(IProjector<ModificationEditViewModel, ModificationUpdateCommand>)).To(typeof(ModificationEditViewModelToModificationUpdateCommandMapper));

            _kernel.Bind(typeof(IProjector<Modification, ModificationViewModel>)).To(typeof(ModificationToModificationViewModelMapper));
            _kernel.Bind(typeof(IProjector<Modification, ModificationEditViewModel>)).To(typeof(ModificationToModificationEditViewModelMapper));

            _kernel.Bind(typeof(IProjector<News, NewsViewModel>)).To(typeof(NewsToNewsViewModelMapper));
            _kernel.Bind(typeof(IProjector<News, NewsEditViewModel>)).To(typeof(NewsToNewsEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<NewsEditViewModel, NewsUpdateCommand>)).To(typeof(NewsEditViewModelToNewsUpdateCommandMapper));

            _kernel.Bind(typeof(IProjector<Comment, CommentViewModel>)).To(typeof(CommentToCommentViewModelMapper));
            _kernel.Bind(typeof(IProjector<Comment, CommentEditViewModel>)).To(typeof(CommentToCommentEditViewModelMapper));

            _kernel.Bind(typeof(IProjector<PlayerSkin, PlayerSkinViewModel>)).To(typeof(PlayerSkinToPlayerSkinViewModelMapper));
            
            _kernel.Bind(typeof(IProjector<Player, PlayerProfileViewModel>)).To(typeof(PlayerToPlayerProfileViewModelMapper));
            _kernel.Bind(typeof(IProjector<PlayerSession, PlayerSessionEditViewModel>)).To(typeof(PlayerSessionToPlayerSessionEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<PlayerSession, JsonSessionData>)).To(typeof(PlayerSessionToJsonSessionDataMapper));
            _kernel.Bind(typeof(IProjector<PlayerSessionEditViewModel, PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionEditViewModelToPlayerSessionUpdateCommandMapper));

            _kernel.Bind(typeof(IProjector<IKTransaction, IKTransactionViewModel>)).To(typeof(IKTransactionToIKTransactionViewModelMapper));
            
            _kernel.Bind(typeof(IProjector<ShopItem, ShopItemEditViewModel>)).To(typeof(ShopItemToShopItemEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItem, ShopItemViewModel>)).To(typeof(ShopItemToShopItemViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryEditViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemCreateCommand>)).To(typeof(ShopItemEditViewModelToShopItemCreateCommandMapper));
            _kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemUpdateCommand>)).To(typeof(ShopItemEditViewModelToShopItemUpdateCommandMapper));
            _kernel.Bind(typeof(IProjector<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryEditViewModelToShopItemCategoryCreateCommandMapper));

            _kernel.Bind(typeof(IProjector<PlayerItem, PlayerItemViewModel>)).To(typeof(PlayerItemToPlayerItemViewModelMapper));

            _kernel.Bind(typeof(IProjector<Currency, CurrencyEditViewModel>)).To(typeof(CurrencyToCurrencyEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<Currency, CurrencyTopViewModel>)).To(typeof(CurrencyToCurrencyTopViewModelMapper));

            _kernel.Bind(typeof(IProjector<Permissions, PermissionsViewModel>)).To(typeof(PermissionsToPermissionsViewModelMapper));

            _kernel.Bind(typeof(IProjector<RulePoint, RulePointViewModel>)).To(typeof(RuleToRuleViewModelMapper));

            _kernel.Bind(typeof(IProjector<Skin, SkinViewModel>)).To(typeof(SkinToSkinViewModelMapper));
            _kernel.Bind(typeof(IProjector<Skin, SkinEditViewModel>)).To(typeof(SkinToSkinEditViewModelMapper));

            _kernel.Bind(typeof(IProjector<AuthLog, AuthLogViewModel>)).To(typeof(AuthLogToAuthLogViewModelMapper));

            _kernel.Bind(typeof(IProjector<Job, JobViewModel>)).To(typeof(JobToJobViewModelMapper));

            _kernel.Bind(typeof(IProjector<News, JsonLauncherNews>)).To(typeof(NewsToJsonLauncherNewsMapper));

            _kernel.Bind(typeof(IProjector<PrivateMessage, PrivateMessageViewModel>)).To(typeof(PrivateMessageToPrivateMessageViewModelMapper));

            #endregion

            _kernel.Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));
        }
    }
}