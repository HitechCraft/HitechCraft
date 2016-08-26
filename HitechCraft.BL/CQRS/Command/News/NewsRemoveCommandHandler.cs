namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class NewsRemoveCommandHandler : BaseCommandHandler<NewsRemoveCommand>
    {
        public NewsRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsRemoveCommand command)
        {
            var newsRep = this.GetRepository<News>();

            newsRep.Delete(command.Id);
            newsRep.Dispose();
        }
    }
}
