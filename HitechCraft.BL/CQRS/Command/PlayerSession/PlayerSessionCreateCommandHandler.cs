using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class PlayerSessionCreateCommandHandler : BaseCommandHandler<PlayerSessionCreateCommand>
    {
        public PlayerSessionCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerSessionCreateCommand command)
        {
            var playerSessionRep = GetRepository<PlayerSession>();
            var playerRep = GetRepository<Player>();
            
            var playerSession = new PlayerSession()
            {
                Md5 = command.Md5,
                Player = playerRep.GetEntity(command.PlayerName),
                Server = command.Server,
                Session = command.Session,
                Token = command.Token
            };

            playerSessionRep.Add(playerSession);
            playerSessionRep.Dispose();
        }
    }
}
