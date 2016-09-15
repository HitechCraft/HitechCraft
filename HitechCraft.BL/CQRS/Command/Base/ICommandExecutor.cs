namespace HitechCraft.BL.CQRS.Command.Base
{
    public interface ICommandExecutor
    {
        void Execute<TCommand>(TCommand command);
    }
}
