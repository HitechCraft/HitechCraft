using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class NewsImageRemoveCommandHandler : BaseCommandHandler<NewsImageRemoveCommand>
    {
        public NewsImageRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsImageRemoveCommand command)
        {
            var newsRep = GetRepository<News>();

            var news = newsRep.GetEntity(command.NewsId);
            news.Image = null;

            newsRep.Update(news);

            newsRep.Dispose();
        }
    }
}
