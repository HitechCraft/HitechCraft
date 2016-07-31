namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ModificationCreateCommandHandler : BaseCommandHandler<ModificationCreateCommand>
    {
        public ModificationCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ModificationCreateCommand command)
        {
            var modRep = this.GetRepository<Modification>();
            
            var mod = new Modification()
            {
                Name = command.Name,
                Description = command.Description,
                Version = command.Version,
                Image = command.Image
            };

            modRep.Add(mod);
            modRep.Dispose();
        }
    }
}
