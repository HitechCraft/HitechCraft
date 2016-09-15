using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class NewsUpdateCommandHandler : BaseCommandHandler<NewsUpdateCommand>
    {
        public NewsUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsUpdateCommand command)
        {
            var newsRep = GetRepository<News>();
            var news = newsRep.GetEntity(command.Id);

            news.Title = command.Title;
            news.Text = command.Text;
            news.Image = command.Image;
            
            newsRep.Update(news);
            newsRep.Dispose();
        }
    }
}
