using System.Linq;
using HitechCraft.DAL.Repository.Specification;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ModificationRemoveCommandHandler : BaseCommandHandler<ModificationRemoveCommand>
    {
        public ModificationRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ModificationRemoveCommand command)
        {
            var modRep = this.GetRepository<Modification>();
            var serverRep = this.GetRepository<Server>();
            var shopItemRep = this.GetRepository<ShopItem>();

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
