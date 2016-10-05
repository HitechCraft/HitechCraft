namespace HitechCraft.Ninjector.Dependences
{
    #region Using Directives
    
    using Core.DI;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using DAL.UnitOfWork;
    using BL.CQRS.Command.Base;
    using Common.Repository;
    using DAL.Repository;
    using Ninject.Modules;

    using Ninject;

    #endregion

    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IContainer)).To(typeof(BaseContainer));

            Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));
            
            #region Command Bindings

            Bind(typeof(ICommandHandler<>)).To(typeof(BaseCommandHandler<>));
            Bind(typeof(ICommandHandler<PlayerRegisterCreateCommand>)).To(typeof(PlayerRegisterCreateCommandHandler));
            Bind(typeof(ICommandHandler<PlayerFixCommand>)).To(typeof(PlayerFixCommandHandler));
            Bind(typeof(ICommandHandler<PlayerInfoUpdateCommand>)).To(typeof(PlayerInfoUpdateCommandHandler));
            Bind(typeof(ICommandHandler<PlayerSkinCreateOrUpdateCommand>)).To(typeof(PlayerSkinCreateOrUpdateCommandHandler));

            Bind(typeof(ICommandHandler<NewsCreateCommand>)).To(typeof(NewsCreateCommandHandler));
            Bind(typeof(ICommandHandler<NewsRemoveCommand>)).To(typeof(NewsRemoveCommandHandler));
            Bind(typeof(ICommandHandler<NewsUpdateCommand>)).To(typeof(NewsUpdateCommandHandler));
            Bind(typeof(ICommandHandler<NewsViewsUpdateCommand>)).To(typeof(NewsViewsUpdateCommandHandler));
            Bind(typeof(ICommandHandler<NewsImageRemoveCommand>)).To(typeof(NewsImageRemoveCommandHandler));

            Bind(typeof(ICommandHandler<CommentCreateCommand>)).To(typeof(CommentCreateCommandHandler));
            Bind(typeof(ICommandHandler<CommentUpdateCommand>)).To(typeof(CommentUpdateCommandHandler));
            Bind(typeof(ICommandHandler<CommentRemoveCommand>)).To(typeof(CommentRemoveCommandHandler));

            Bind(typeof(ICommandHandler<ServerCreateCommand>)).To(typeof(ServerCreateCommandHandler));
            Bind(typeof(ICommandHandler<ServerUpdateCommand>)).To(typeof(ServerUpdateCommandHandler));
            Bind(typeof(ICommandHandler<ServerRemoveCommand>)).To(typeof(ServerRemoveCommandHandler));

            Bind(typeof(ICommandHandler<ModificationCreateCommand>)).To(typeof(ModificationCreateCommandHandler));
            Bind(typeof(ICommandHandler<ModificationUpdateCommand>)).To(typeof(ModificationUpdateCommandHandler));
            Bind(typeof(ICommandHandler<ModificationRemoveCommand>)).To(typeof(ModificationRemoveCommandHandler));

            Bind(typeof(ICommandHandler<IKTransactionCreateCommand>)).To(typeof(IKTransactionCreateCommandHandler));
            Bind(typeof(ICommandHandler<IKTransactionUpdateCommand>)).To(typeof(IKTransactionUpdateCommandHandler));

            Bind(typeof(ICommandHandler<CurrencyCreateCommand>)).To(typeof(CurrencyCreateCommandHandler));
            Bind(typeof(ICommandHandler<CurrencyUpdateCommand>)).To(typeof(CurrencyUpdateCommandHandler));

            Bind(typeof(ICommandHandler<PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionUpdateCommandHandler));
            Bind(typeof(ICommandHandler<PlayerSessionCreateCommand>)).To(typeof(PlayerSessionCreateCommandHandler));

            Bind(typeof(ICommandHandler<ShopItemCreateCommand>)).To(typeof(ShopItemCreateCommandHandler));
            Bind(typeof(ICommandHandler<ShopItemRemoveCommand>)).To(typeof(ShopItemRemoveCommandHandler));
            Bind(typeof(ICommandHandler<ShopItemUpdateCommand>)).To(typeof(ShopItemUpdateCommandHandler));
            Bind(typeof(ICommandHandler<ShopItemBuyCommand>)).To(typeof(ShopItemBuyCommandHandler));
            Bind(typeof(ICommandHandler<ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryCreateCommandHandler));
            Bind(typeof(ICommandHandler<ShopItemCategoryRemoveCommand>)).To(typeof(ShopItemCategoryRemoveCommandHandler));

            Bind(typeof(ICommandHandler<ShopItemAddRandomCommand>)).To(typeof(ShopItemAddRandomCommandHandler));

            Bind(typeof(ICommandHandler<DonateGroupIEBuyCommand>)).To(typeof(DonateGroupIEBuyCommandHandler));

            Bind(typeof(ICommandHandler<AuthLogCreateCommand>)).To(typeof(AuthLogCreateCommandHandler));
            
            Bind(typeof(ICommandHandler<RuleCreateCommand>)).To(typeof(RuleCreateCommandHandler));
            Bind(typeof(ICommandHandler<RuleRemoveCommand>)).To(typeof(RuleRemoveCommandHandler));
            Bind(typeof(ICommandHandler<RuleUpdateCommand>)).To(typeof(RuleUpdateCommandHandler));

            Bind(typeof(ICommandHandler<RulePointCreateCommand>)).To(typeof(RulePointCreateCommandHandler));
            Bind(typeof(ICommandHandler<RulePointUpdateCommand>)).To(typeof(RulePointUpdateCommandHandler));
            Bind(typeof(ICommandHandler<RulePointRemoveCommand>)).To(typeof(RulePointRemoveCommandHandler));

            Bind(typeof(ICommandHandler<SkinCreateCommand>)).To(typeof(SkinCreateCommandHandler));

            Bind(typeof(ICommandHandler<SkinInstallCommand>)).To(typeof(SkinInstallCommandHandler));
            
            Bind(typeof(ICommandHandler<PMInboxReadCommand>)).To(typeof(PMInboxReadCommandHandler));
            Bind(typeof(ICommandHandler<PMInboxRemoveCommand>)).To(typeof(PMInboxRemoveCommandHandler));

            Bind(typeof(ICommandHandler<ReferPayCommand>)).To(typeof(ReferPayCommandHandler));

            #endregion

            Bind(typeof(ICommandExecutor)).To(typeof(CommandExecutor));
        
            Bind(typeof(IQueryHandler<,>)).To(typeof(EntityListQueryHandler<,>));
            Bind(typeof(IQueryHandler<,>)).To(typeof(EntityQueryHandler<,>));
            Bind(typeof(IQueryHandler<,>)).To(typeof(PlayerByLoginQueryHandler));
            
            Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));
        }
    }
}