namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ServerCreateCommandHandler : BaseCommandHandler<ServerCreateCommand>
    {
        public ServerCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ServerCreateCommand command)
        {
            var serverRep = this.GetRepository<Server>();

            var server = this.GetProjector<ServerCreateCommand, Server>().Project(command);

            serverRep.Add(server);
            serverRep.Dispose();
        }
    }
}
