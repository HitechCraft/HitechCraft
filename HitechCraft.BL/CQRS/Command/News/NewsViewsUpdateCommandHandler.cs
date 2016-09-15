using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Core.DI;
    using Core.Entity;

    #endregion

    public class NewsViewsUpdateCommandHandler : BaseCommandHandler<NewsViewsUpdateCommand>
    {
        public NewsViewsUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsViewsUpdateCommand command)
        {
            var newsRep = GetRepository<News>();

            var news = newsRep.GetEntity(command.NewsId);
            news.ViewersCount++;

            newsRep.Update(news);

            newsRep.Dispose();
        }
    }
}
