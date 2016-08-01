namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using DAL.Repository.Specification;
    using System.Linq;
    #endregion

    public class PlayerSkinCreateOrUpdateCommandHandler : BaseCommandHandler<PlayerSkinCreateOrUpdateCommand>
    {
        public PlayerSkinCreateOrUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerSkinCreateOrUpdateCommand command)
        {
            var playerSkinRep = this.GetRepository<PlayerSkin>();
            var playerRep = this.GetRepository<Player>();

            PlayerSkin playerSkin;

            try
            {
                playerSkin = playerSkinRep.Query(new PlayerSkinByUserNameSpec(command.PlayerName)).First();

                playerSkin.Image = command.Image;

                playerSkinRep.Update(playerSkin);

                playerSkinRep.Dispose();
            }
            catch (System.Exception)
            {
                playerSkin = new PlayerSkin()
                {
                    Image = command.Image,
                    Player = playerRep.GetEntity(command.PlayerName)
                };

                playerSkinRep.Add(playerSkin);
                playerSkinRep.Dispose();
            }
        }
    }
}
