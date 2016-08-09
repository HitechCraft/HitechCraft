namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class NewsImageRemoveCommandHandler : BaseCommandHandler<NewsImageRemoveCommand>
    {
        public NewsImageRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsImageRemoveCommand command)
        {
            var newsRep = this.GetRepository<News>();

            var news = newsRep.GetEntity(command.NewsId);
            news.Image = null;

            newsRep.Update(news);

            newsRep.Dispose();
        }
    }
}
