namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class PlayerSessionCreateCommandHandler : BaseCommandHandler<PlayerSessionCreateCommand>
    {
        public PlayerSessionCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerSessionCreateCommand command)
        {
            var playerSessionRep = this.GetRepository<PlayerSession>();
            var playerRep = this.GetRepository<Player>();
            
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
