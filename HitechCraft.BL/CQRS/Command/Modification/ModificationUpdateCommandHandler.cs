using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class ModificationUpdateCommandHandler : BaseCommandHandler<ModificationUpdateCommand>
    {
        public ModificationUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ModificationUpdateCommand command)
        {
            var modRep = GetRepository<Modification>();

            var mod = modRep.GetEntity(command.Id);

            //TODO: перенести в автомаппер
            mod.Name = command.Name;
            mod.Description = command.Description;
            mod.Version = command.Version;
            mod.Image = command.Image;
            mod.GuideVideo = command.GuideVideo;

            modRep.Update(mod);
            modRep.Dispose();
        }
    }
}
