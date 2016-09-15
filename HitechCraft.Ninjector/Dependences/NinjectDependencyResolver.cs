namespace HitechCraft.Ninjector.Dependences
{
    #region Using Directives
    
    using Core.DI;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using DAL.UnitOfWork;
    using BL.CQRS.Command.Base;
    using Common.Repository;
    using Common.UnitOfWork;
    using DAL.Repository;

    using Ninject;

    #endregion

    public class NinjectDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }
        
        public void AddBindings()
        {
            _kernel.Bind(typeof(IContainer)).To(typeof(BaseContainer));

            _kernel.Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));
            
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
            
            _kernel.Bind(typeof(IUnitOfWork)).To(typeof(NHibernateUnitOfWork));
        }
    }
}