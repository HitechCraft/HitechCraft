namespace HitechCraft.WebApplication.Ninject
{
    using BL.CQRS.Command;
    using Core.Entity;
    using Core.Models.Json;
    using Projector.Impl;
    using Mapper;
    using Models;
    using Current;

    using global::Ninject.Modules;

    public class WebAppModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ICurrentUser)).To(typeof(CurrentUser));

            #region Projector Ninjects

            Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));

            Bind(typeof(IProjector<Ban, BanViewModel>)).To(typeof(BanToBanViewModelMapper));

            Bind(typeof(IProjector<Server, ServerViewModel>)).To(typeof(ServerToServerViewModelMapper));
            Bind(typeof(IProjector<Server, ServerEditViewModel>)).To(typeof(ServerToServerEditViewModelMapper));
            Bind(typeof(IProjector<ServerEditViewModel, ServerCreateCommand>)).To(typeof(ServerEditViewModelToServerCreateCommandMapper));
            Bind(typeof(IProjector<ServerEditViewModel, ServerUpdateCommand>)).To(typeof(ServerEditViewModelToServerUpdateCommandMapper));
            Bind(typeof(IProjector<ServerCreateCommand, Server>)).To(typeof(ServerCreateCommandToServerMapper));

            Bind(typeof(IProjector<Modification, ServerModificationViewModel>)).To(typeof(ServerToServerViewModelMapper));
            Bind(typeof(IProjector<ModificationEditViewModel, ModificationCreateCommand>)).To(typeof(ModificationEditViewModelToModificationCreateCommandMapper));
            Bind(typeof(IProjector<ModificationEditViewModel, ModificationUpdateCommand>)).To(typeof(ModificationEditViewModelToModificationUpdateCommandMapper));

            Bind(typeof(IProjector<Modification, ModificationViewModel>)).To(typeof(ModificationToModificationViewModelMapper));
            Bind(typeof(IProjector<Modification, ModificationEditViewModel>)).To(typeof(ModificationToModificationEditViewModelMapper));

            Bind(typeof(IProjector<News, NewsViewModel>)).To(typeof(NewsToNewsViewModelMapper));
            Bind(typeof(IProjector<News, NewsEditViewModel>)).To(typeof(NewsToNewsEditViewModelMapper));
            Bind(typeof(IProjector<NewsEditViewModel, NewsUpdateCommand>)).To(typeof(NewsEditViewModelToNewsUpdateCommandMapper));

            Bind(typeof(IProjector<Comment, CommentViewModel>)).To(typeof(CommentToCommentViewModelMapper));
            Bind(typeof(IProjector<Comment, CommentEditViewModel>)).To(typeof(CommentToCommentEditViewModelMapper));

            Bind(typeof(IProjector<PlayerSkin, PlayerSkinViewModel>)).To(typeof(PlayerSkinToPlayerSkinViewModelMapper));

            Bind(typeof(IProjector<Player, PlayerProfileViewModel>)).To(typeof(PlayerToPlayerProfileViewModelMapper));
            Bind(typeof(IProjector<PlayerSession, PlayerSessionEditViewModel>)).To(typeof(PlayerSessionToPlayerSessionEditViewModelMapper));
            Bind(typeof(IProjector<PlayerSession, JsonSessionData>)).To(typeof(PlayerSessionToJsonSessionDataMapper));
            Bind(typeof(IProjector<PlayerSessionEditViewModel, PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionEditViewModelToPlayerSessionUpdateCommandMapper));

            Bind(typeof(IProjector<IKTransaction, IKTransactionViewModel>)).To(typeof(IKTransactionToIKTransactionViewModelMapper));

            Bind(typeof(IProjector<ShopItem, ShopItemEditViewModel>)).To(typeof(ShopItemToShopItemEditViewModelMapper));
            Bind(typeof(IProjector<ShopItem, ShopItemViewModel>)).To(typeof(ShopItemToShopItemViewModelMapper));
            Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryEditViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryEditViewModelMapper));
            Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryViewModelMapper));
            Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemCreateCommand>)).To(typeof(ShopItemEditViewModelToShopItemCreateCommandMapper));
            Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemUpdateCommand>)).To(typeof(ShopItemEditViewModelToShopItemUpdateCommandMapper));
            Bind(typeof(IProjector<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryEditViewModelToShopItemCategoryCreateCommandMapper));

            Bind(typeof(IProjector<PlayerItem, PlayerItemViewModel>)).To(typeof(PlayerItemToPlayerItemViewModelMapper));

            Bind(typeof(IProjector<Currency, CurrencyEditViewModel>)).To(typeof(CurrencyToCurrencyEditViewModelMapper));
            Bind(typeof(IProjector<Currency, CurrencyTopViewModel>)).To(typeof(CurrencyToCurrencyTopViewModelMapper));

            Bind(typeof(IProjector<Permissions, PermissionsViewModel>)).To(typeof(PermissionsToPermissionsViewModelMapper));

            Bind(typeof(IProjector<RulePoint, RulePointViewModel>)).To(typeof(RuleToRuleViewModelMapper));

            Bind(typeof(IProjector<Skin, SkinViewModel>)).To(typeof(SkinToSkinViewModelMapper));
            Bind(typeof(IProjector<Skin, SkinEditViewModel>)).To(typeof(SkinToSkinEditViewModelMapper));

            Bind(typeof(IProjector<AuthLog, AuthLogViewModel>)).To(typeof(AuthLogToAuthLogViewModelMapper));

            Bind(typeof(IProjector<Job, JobViewModel>)).To(typeof(JobToJobViewModelMapper));

            Bind(typeof(IProjector<News, JsonLauncherNews>)).To(typeof(NewsToJsonLauncherNewsMapper));

            Bind(typeof(IProjector<PrivateMessage, PrivateMessageViewModel>)).To(typeof(PrivateMessageToPrivateMessageViewModelMapper));

            #endregion
        }
    }
}