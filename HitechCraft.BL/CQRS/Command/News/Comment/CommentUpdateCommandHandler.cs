namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class CommentUpdateCommandHandler : BaseCommandHandler<CommentUpdateCommand>
    {
        public CommentUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CommentUpdateCommand command)
        {
            var commentRep = this.GetRepository<Comment>();
            
            var comment = commentRep.GetEntity(command.Id);
            comment.Text = command.Text;

            commentRep.Update(comment);
            commentRep.Dispose();
        }
    }
}
