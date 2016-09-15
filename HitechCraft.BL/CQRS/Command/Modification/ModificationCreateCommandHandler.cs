using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.Entity;
    using Core.DI;

    #endregion

    public class ModificationCreateCommandHandler : BaseCommandHandler<ModificationCreateCommand>
    {
        public ModificationCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ModificationCreateCommand command)
        {
            var modRep = GetRepository<Modification>();
            
            var mod = new Modification()
            {
                Name = command.Name,
                Description = command.Description,
                Version = command.Version,
                Image = command.Image,
                GuideVideo = command.GuideVideo
        };

            modRep.Add(mod);
            modRep.Dispose();
        }
    }
}
