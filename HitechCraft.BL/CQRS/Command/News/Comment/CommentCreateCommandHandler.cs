using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class CommentCreateCommandHandler : BaseCommandHandler<CommentCreateCommand>
    {
        public CommentCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CommentCreateCommand command)
        {
            var newsRep = GetRepository<News>();
            var playerRep = GetRepository<Player>();
            var commentRep = GetRepository<Comment>();

            var author = playerRep.GetEntity(command.AuthorName);
            var news = newsRep.GetEntity(command.NewsId);

            var comment = new Comment()
            {
                Author = author,
                News = news,
                Text = command.Text,
                TimeCreate = DateTime.Now
            };

            commentRep.Add(comment);
            commentRep.Dispose();
        }
    }
}
