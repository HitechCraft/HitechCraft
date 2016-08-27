namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ShopItemRemoveCommandHandler : BaseCommandHandler<ShopItemRemoveCommand>
    {
        public ShopItemRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemRemoveCommand command)
        {
            var shopItemRep = this.GetRepository<ShopItem>();

            shopItemRep.Delete(command.GameId);
            shopItemRep.Dispose();
        }
    }
}
