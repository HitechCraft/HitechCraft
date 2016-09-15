namespace HitechCraft.BL.CQRS.Command.Base
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}
