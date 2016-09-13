namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;
    using System.Linq;
    using DAL.Repository.Specification;

    #endregion

    public class ServerRemoveCommandHandler : BaseCommandHandler<ServerRemoveCommand>
    {
        public ServerRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ServerRemoveCommand command)
        {
            var serverRep = this.GetRepository<Server>();
            var modRep = this.GetRepository<Modification>();

            if(modRep.Query(new ModificationByServerSpec(command.Id)).Any())
                throw new Exception("Сервер связан с модификациями");

            serverRep.Delete(command.Id);

            serverRep.Dispose();
            modRep.Dispose();
        }
    }
}
