namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ServerRemoveCommandHandler : BaseCommandHandler<ServerRemoveCommand>
    {
        public ServerRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ServerRemoveCommand command)
        {
            var serverRep = this.GetRepository<Server>();
            
            serverRep.Delete(command.Id);
            serverRep.Dispose();
        }
    }
}
