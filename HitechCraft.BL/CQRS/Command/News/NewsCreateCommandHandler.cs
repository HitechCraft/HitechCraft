namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class NewsCreateCommandHandler : BaseCommandHandler<NewsCreateCommand>
    {
        public NewsCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsCreateCommand command)
        {
            var newsRep = this.GetRepository<News>();
            var playerRep = this.GetRepository<Player>();

            var author = playerRep.GetEntity(command.PlayerName);

            var news = new News()
            {
                Title = command.Title,
                Text = command.Text,
                Author = author,
                Image = command.Image,
                TimeCreate = DateTime.Now,
                ViewersCount = 1
            };

            newsRep.Add(news);
            newsRep.Dispose();
        }
    }
}
