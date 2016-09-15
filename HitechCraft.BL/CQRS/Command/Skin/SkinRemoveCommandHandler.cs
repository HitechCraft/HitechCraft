using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class SkinRemoveCommandHandler : BaseCommandHandler<SkinRemoveCommand>
    {
        public SkinRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(SkinRemoveCommand command)
        {
            var skinRep = GetRepository<Skin>();

            skinRep.Delete(command.Id);
            skinRep.Dispose();
        }
    }
}

