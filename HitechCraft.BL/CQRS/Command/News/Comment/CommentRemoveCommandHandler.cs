namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class CommentRemoveCommandHandler : BaseCommandHandler<CommentRemoveCommand>
    {
        public CommentRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CommentRemoveCommand command)
        {
            var commentRep = this.GetRepository<Comment>();
            
            commentRep.Delete(command.Id);
            commentRep.Dispose();
        }
    }
}
