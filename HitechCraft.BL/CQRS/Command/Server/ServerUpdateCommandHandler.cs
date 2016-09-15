using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class ServerUpdateCommandHandler : BaseCommandHandler<ServerUpdateCommand>
    {
        public ServerUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ServerUpdateCommand command)
        {
            var serverRep = GetRepository<Server>();

            var server = serverRep.GetEntity(command.Id);

            //TODO: сделать через автомаппер
            server.Name = command.Name;
            server.Description = command.Description;
            server.ClientVersion = command.ClientVersion;
            if(command.Image != null) server.Image = command.Image;
            server.IpAddress = command.IpAddress;
            server.Port = command.Port;
            server.MapPort = command.MapPort;

            serverRep.Update(server);
            serverRep.Dispose();
        }
    }
}
