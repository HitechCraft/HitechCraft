namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class CommentCreateCommandHandler : BaseCommandHandler<CommentCreateCommand>
    {
        public CommentCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CommentCreateCommand command)
        {
            var newsRep = this.GetRepository<News>();
            var playerRep = this.GetRepository<Player>();
            var commentRep = this.GetRepository<Comment>();

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
