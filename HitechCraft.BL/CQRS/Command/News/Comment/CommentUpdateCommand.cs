namespace HitechCraft.BL.CQRS.Command
{
    public class CommentUpdateCommand
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
