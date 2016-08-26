namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class NewsViewsUpdateCommandHandler : BaseCommandHandler<NewsViewsUpdateCommand>
    {
        public NewsViewsUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsViewsUpdateCommand command)
        {
            var newsRep = this.GetRepository<News>();

            var news = newsRep.GetEntity(command.NewsId);
            news.ViewersCount++;

            newsRep.Update(news);

            newsRep.Dispose();
        }
    }
}
