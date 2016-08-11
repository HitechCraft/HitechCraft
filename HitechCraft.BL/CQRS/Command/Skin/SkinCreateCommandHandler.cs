namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;

    #endregion

    public class SkinCreateCommandHandler : BaseCommandHandler<SkinCreateCommand>
    {
        public SkinCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(SkinCreateCommand command)
        {
            var skinRep = GetRepository<Skin>();
            
            var skin = new Skin()
            {
                Name = command.Name,
                Image = command.Image,
                Gender = command.Gender
            };

            skinRep.Add(skin);

            skinRep.Dispose();
        }
    }
}

