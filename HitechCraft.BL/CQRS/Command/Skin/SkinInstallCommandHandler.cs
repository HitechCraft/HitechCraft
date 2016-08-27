namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;
    using DAL.Repository.Specification;
    using System.Linq;

    #endregion

    public class SkinInstallCommandHandler : BaseCommandHandler<SkinInstallCommand>
    {
        public SkinInstallCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(SkinInstallCommand command)
        {
            var skinRep = GetRepository<Skin>();
            var playerSkinRep = GetRepository<PlayerSkin>();
            var playerRep = GetRepository<Player>();

            var skinImage = skinRep.GetEntity(command.SkinId).Image;

            try
            {
                var playerSkin = playerSkinRep.Query(new PlayerSkinByUserNameSpec(command.PlayerName)).First();

                playerSkin.Image = skinImage;
                playerSkinRep.Update(playerSkin);
            }
            catch
            {
                var playerSkin = new PlayerSkin()
                {
                    Player = playerRep.GetEntity(command.PlayerName),
                    Image = skinImage
                };

                playerSkinRep.Add(playerSkin);
            }

            playerSkinRep.Dispose();
            skinRep.Dispose();
            playerRep.Dispose();
        }
    }
}

