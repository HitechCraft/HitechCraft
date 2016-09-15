using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class ServerCreateCommandHandler : BaseCommandHandler<ServerCreateCommand>
    {
        public ServerCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ServerCreateCommand command)
        {
            var serverRep = GetRepository<Server>();

            var server = GetProjector<ServerCreateCommand, Server>().Project(command);

            serverRep.Add(server);
            serverRep.Dispose();
        }
    }
}
