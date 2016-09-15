using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class NewsCreateCommandHandler : BaseCommandHandler<NewsCreateCommand>
    {
        public NewsCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(NewsCreateCommand command)
        {
            var newsRep = GetRepository<News>();
            var playerRep = GetRepository<Player>();

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
