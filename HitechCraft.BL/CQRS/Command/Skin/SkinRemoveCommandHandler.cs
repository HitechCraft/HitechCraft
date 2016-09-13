namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;

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

