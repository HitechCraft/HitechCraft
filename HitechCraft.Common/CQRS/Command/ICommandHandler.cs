namespace HitechCraft.Common.CQRS.Command
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}
