namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ModificationUpdateCommandHandler : BaseCommandHandler<ModificationUpdateCommand>
    {
        public ModificationUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ModificationUpdateCommand command)
        {
            var modRep = this.GetRepository<Modification>();

            var mod = modRep.GetEntity(command.Id);

            //TODO: перенести в автомаппер
            mod.Name = command.Name;
            mod.Description = command.Description;
            mod.Version = command.Version;

            modRep.Update(mod);
            modRep.Dispose();
        }
    }
}
