namespace HitechCraft.WebAdmin.Ninject
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Ninjector.Dependences;
    using BL.CQRS.Command;
    using Core.Entity;
    using Projector.Impl;
    using Mapper;
    using Models;

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
            new NinjectDependencyResolver(_kernel).AddBindings();

            #region Projector Ninjects

            _kernel.Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));
            
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
            
            _kernel.Bind(typeof(IProjector<PlayerSkin, PlayerSkinViewModel>)).To(typeof(PlayerSkinToPlayerSkinViewModelMapper));
            
            _kernel.Bind(typeof(IProjector<ShopItem, ShopItemEditViewModel>)).To(typeof(ShopItemToShopItemEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItem, ShopItemViewModel>)).To(typeof(ShopItemToShopItemViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryEditViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryEditViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItemCategory, ShopItemCategoryViewModel>)).To(typeof(ShopItemCategoryToShopItemCategoryViewModelMapper));
            _kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemCreateCommand>)).To(typeof(ShopItemEditViewModelToShopItemCreateCommandMapper));
            _kernel.Bind(typeof(IProjector<ShopItemEditViewModel, ShopItemUpdateCommand>)).To(typeof(ShopItemEditViewModelToShopItemUpdateCommandMapper));
            _kernel.Bind(typeof(IProjector<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>)).To(typeof(ShopItemCategoryEditViewModelToShopItemCategoryCreateCommandMapper));
            
            _kernel.Bind(typeof(IProjector<RulePoint, RulePointViewModel>)).To(typeof(RuleToRuleViewModelMapper));

            _kernel.Bind(typeof(IProjector<Skin, SkinViewModel>)).To(typeof(SkinToSkinViewModelMapper));
            _kernel.Bind(typeof(IProjector<Skin, SkinEditViewModel>)).To(typeof(SkinToSkinEditViewModelMapper));
            
            #endregion
        }
    }
}