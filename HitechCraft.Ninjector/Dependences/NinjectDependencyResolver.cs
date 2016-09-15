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

    using Ninject;

    #endregion

    public static class NinjectDependencyResolver
    {
        public static IKernel AddBindings(IKernel kernel)
        {
            kernel.Bind(typeof(IContainer)).To(typeof(BaseContainer));

            kernel.Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));
            
            #region Command Bindings

            kernel.Bind(typeof(ICommandHandler<>)).To(typeof(BaseCommandHandler<>));
            kernel.Bind(typeof(ICommandHandler<PlayerRegisterCreateCommand>)).To(typeof(PlayerRegisterCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<PlayerSkinCreateOrUpdateCommand>)).To(typeof(PlayerSkinCreateOrUpdateCommandHandler));

            kernel.Bind(typeof(ICommandHandler<NewsCreateCommand>)).To(typeof(NewsCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<NewsRemoveCommand>)).To(typeof(NewsRemoveCommandHandler));
            kernel.Bind(typeof(ICommandHandler<NewsUpdateCommand>)).To(typeof(NewsUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<NewsViewsUpdateCommand>)).To(typeof(NewsViewsUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<NewsImageRemoveCommand>)).To(typeof(NewsImageRemoveCommandHandler));

            kernel.Bind(typeof(ICommandHandler<CommentCreateCommand>)).To(typeof(CommentCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<CommentUpdateCommand>)).To(typeof(CommentUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<CommentRemoveCommand>)).To(typeof(CommentRemoveCommandHandler));

            kernel.Bind(typeof(ICommandHandler<ServerCreateCommand>)).To(typeof(ServerCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ServerUpdateCommand>)).To(typeof(ServerUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ServerRemoveCommand>)).To(typeof(ServerRemoveCommandHandler));

            kernel.Bind(typeof(ICommandHandler<ModificationCreateCommand>)).To(typeof(ModificationCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ModificationUpdateCommand>)).To(typeof(ModificationUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ModificationRemoveCommand>)).To(typeof(ModificationRemoveCommandHandler));

            kernel.Bind(typeof(ICommandHandler<IKTransactionCreateCommand>)).To(typeof(IKTransactionCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<IKTransactionUpdateCommand>)).To(typeof(IKTransactionUpdateCommandHandler));

            kernel.Bind(typeof(ICommandHandler<CurrencyCreateCommand>)).To(typeof(CurrencyCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<CurrencyUpdateCommand>)).To(typeof(CurrencyUpdateCommandHandler));

            kernel.Bind(typeof(ICommandHandler<PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<PlayerSessionCreateCommand>)).To(typeof(PlayerSessionCreateCommandHandler));

            kernel.Bind(typeof(ICommandHandler<ShopItemCreateCommand>)).To(typeof(ShopItemCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ShopItemRemoveCommand>)).To(typeof(ShopItemRemoveCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ShopItemUpdateCommand>)).To(typeof(ShopItemUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ShopItemBuyCommand>)).To(typeof(ShopItemBuyCommandHandler));
            kernel.Bind(typeof(ICommandHandler<ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryCreateCommandHandler));

            kernel.Bind(typeof(ICommandHandler<ShopItemAddRandomCommand>)).To(typeof(ShopItemAddRandomCommandHandler));

            kernel.Bind(typeof(ICommandHandler<DonateGroupIEBuyCommand>)).To(typeof(DonateGroupIEBuyCommandHandler));

            kernel.Bind(typeof(ICommandHandler<AuthLogCreateCommand>)).To(typeof(AuthLogCreateCommandHandler));
            
            kernel.Bind(typeof(ICommandHandler<RuleCreateCommand>)).To(typeof(RuleCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<RuleRemoveCommand>)).To(typeof(RuleRemoveCommandHandler));
            kernel.Bind(typeof(ICommandHandler<RuleUpdateCommand>)).To(typeof(RuleUpdateCommandHandler));

            kernel.Bind(typeof(ICommandHandler<RulePointCreateCommand>)).To(typeof(RulePointCreateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<RulePointUpdateCommand>)).To(typeof(RulePointUpdateCommandHandler));
            kernel.Bind(typeof(ICommandHandler<RulePointRemoveCommand>)).To(typeof(RulePointRemoveCommandHandler));

            kernel.Bind(typeof(ICommandHandler<SkinCreateCommand>)).To(typeof(SkinCreateCommandHandler));

            kernel.Bind(typeof(ICommandHandler<SkinInstallCommand>)).To(typeof(SkinInstallCommandHandler));
            
            kernel.Bind(typeof(ICommandHandler<PMInboxReadCommand>)).To(typeof(PMInboxReadCommandHandler));
            kernel.Bind(typeof(ICommandHandler<PMInboxRemoveCommand>)).To(typeof(PMInboxRemoveCommandHandler));

            #endregion

            kernel.Bind(typeof(ICommandExecutor)).To(typeof(CommandExecutor));
        
            kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityListQueryHandler<,>));
            kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(EntityQueryHandler<,>));
            kernel.Bind(typeof(IQueryHandler<,>)).To(typeof(PlayerByLoginQueryHandler));
            
            kernel.Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));

            return kernel;
        }
    }
}