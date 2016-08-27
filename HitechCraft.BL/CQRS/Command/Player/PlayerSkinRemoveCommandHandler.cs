namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using DAL.Repository.Specification;
    using System.Linq;
    #endregion

    public class PlayerSkinRemoveCommandHandler : BaseCommandHandler<PlayerSkinRemoveCommand>
    {
        public PlayerSkinRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerSkinRemoveCommand command)
        {
            var playerSkinRep = this.GetRepository<PlayerSkin>();
            
            try
            {
                var playerSkin = playerSkinRep.Query(new PlayerSkinByUserNameSpec(command.PlayerName)).First();

                playerSkin.Image = null;
                playerSkinRep.Update(playerSkin);

                playerSkinRep.Dispose();
            }
            catch (System.Exception)
            {
                playerSkinRep.Dispose();
            }
        }
    }
}
