namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class PlayerSessionUpdateCommandHandler : BaseCommandHandler<PlayerSessionUpdateCommand>
    {
        public PlayerSessionUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerSessionUpdateCommand command)
        {
            var playerSessionRep = this.GetRepository<PlayerSession>();

            var playerSession = playerSessionRep.GetEntity(command.Id);

            playerSession.Server = command.Server;
            playerSession.Session = command.Session;
            playerSession.Token = command.Token;

            playerSessionRep.Update(playerSession);
            playerSessionRep.Dispose();
        }
    }
}
