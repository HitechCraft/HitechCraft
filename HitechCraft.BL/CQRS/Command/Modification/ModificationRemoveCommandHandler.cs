namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ModificationRemoveCommandHandler : BaseCommandHandler<ModificationRemoveCommand>
    {
        public ModificationRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ModificationRemoveCommand command)
        {
            var modRep = this.GetRepository<Modification>();
            
            modRep.Delete(command.Id);
            modRep.Dispose();
        }
    }
}
