using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class NewsRemoveCommandHandler : BaseCommandHandler<NewsRemoveCommand>
    {
        public NewsRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsRemoveCommand command)
        {
            var newsRep = GetRepository<News>();

            newsRep.Delete(command.Id);
            newsRep.Dispose();
        }
    }
}
