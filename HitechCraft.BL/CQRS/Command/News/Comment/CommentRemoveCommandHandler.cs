using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class CommentRemoveCommandHandler : BaseCommandHandler<CommentRemoveCommand>
    {
        public CommentRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CommentRemoveCommand command)
        {
            var commentRep = GetRepository<Comment>();
            
            commentRep.Delete(command.Id);
            commentRep.Dispose();
        }
    }
}
