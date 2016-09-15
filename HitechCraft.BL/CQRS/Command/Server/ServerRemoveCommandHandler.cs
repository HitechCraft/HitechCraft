using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.Repository.Specification.Modification;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;
    using System.Linq;

    #endregion

    public class ServerRemoveCommandHandler : BaseCommandHandler<ServerRemoveCommand>
    {
        public ServerRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ServerRemoveCommand command)
        {
            var serverRep = GetRepository<Server>();
            var modRep = GetRepository<Modification>();

            if(modRep.Query(new ModificationByServerSpec(command.Id)).Any())
                throw new Exception("Сервер связан с модификациями");

            serverRep.Delete(command.Id);

            serverRep.Dispose();
            modRep.Dispose();
        }
    }
}
