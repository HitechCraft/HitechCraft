namespace HitechCraft.Common.CQRS.Command
{
    #region Using Directives

    using DI;

    #endregion

    public class CommandExecutor : ICommandExecutor
    {
        private readonly IContainer _container;

        public CommandExecutor(IContainer container)
        {
            this._container = container;
        }

        public void Execute<TCommand>(TCommand command)
        {
            var handler = this._container.Resolve<ICommandHandler<TCommand>>();

            handler.Handle(command);
        }
    }
}
