namespace HitechCraft.Common.CQRS.Command
{
    public interface ICommandExecutor
    {
        void Execute<TCommand>(TCommand command);
    }
}
