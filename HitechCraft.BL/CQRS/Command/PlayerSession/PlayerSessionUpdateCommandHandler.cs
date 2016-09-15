using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class PlayerSessionUpdateCommandHandler : BaseCommandHandler<PlayerSessionUpdateCommand>
    {
        public PlayerSessionUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerSessionUpdateCommand command)
        {
            var playerSessionRep = GetRepository<PlayerSession>();

            var playerSession = playerSessionRep.GetEntity(command.Id);

            playerSession.Server = command.Server;
            playerSession.Session = command.Session;
            playerSession.Token = command.Token;

            playerSessionRep.Update(playerSession);
            playerSessionRep.Dispose();
        }
    }
}
