using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.Repository.Specification.Server;
using HitechCraft.Core.Repository.Specification.Shop;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using System.Linq;
    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class ModificationRemoveCommandHandler : BaseCommandHandler<ModificationRemoveCommand>
    {
        public ModificationRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ModificationRemoveCommand command)
        {
            var modRep = GetRepository<Modification>();
            var serverRep = GetRepository<Server>();
            var shopItemRep = GetRepository<ShopItem>();

            if (serverRep.Query(new ServerByModificationSpec(command.Id)).Any())
            {
                throw new Exception("Модификация связана с сервером");
            }

            if (shopItemRep.Query(new ShopItemByModificationSpec(command.Id)).Any())
            {
                throw new Exception("Модификация связана с предметами из Магазина ресурсов");
            }

            modRep.Delete(command.Id);

            modRep.Dispose();
            serverRep.Dispose();
            shopItemRep.Dispose();
        }
    }
}
