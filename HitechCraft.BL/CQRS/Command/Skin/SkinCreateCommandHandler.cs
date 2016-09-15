using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

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

