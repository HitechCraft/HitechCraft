namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class CommentCreateCommand
    {
        public int NewsId { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }
    }
}
