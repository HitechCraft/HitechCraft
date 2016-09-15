using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    
    #endregion

    public class CommentUpdateCommandHandler : BaseCommandHandler<CommentUpdateCommand>
    {
        public CommentUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CommentUpdateCommand command)
        {
            var commentRep = GetRepository<Comment>();
            
            var comment = commentRep.GetEntity(command.Id);
            comment.Text = command.Text;

            commentRep.Update(comment);
            commentRep.Dispose();
        }
    }
}
