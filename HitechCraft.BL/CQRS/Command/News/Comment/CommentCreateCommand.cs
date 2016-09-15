namespace HitechCraft.BL.CQRS.Command
{
    public class CommentCreateCommand
    {
        public int NewsId { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }
    }
}
