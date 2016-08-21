namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class NewsUpdateCommandHandler : BaseCommandHandler<NewsUpdateCommand>
    {
        public NewsUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsUpdateCommand command)
        {
            var newsRep = this.GetRepository<News>();
            var news = newsRep.GetEntity(command.Id);

            news.Title = command.Title;
            news.Text = command.Text;
            news.Image = command.Image;
            
            newsRep.Update(news);
            newsRep.Dispose();
        }
    }
}
