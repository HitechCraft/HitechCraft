using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
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
            var playerSkinRep = GetRepository<PlayerSkin>();
            
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
