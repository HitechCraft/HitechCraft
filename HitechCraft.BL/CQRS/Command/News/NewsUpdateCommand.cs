namespace HitechCraft.BL.CQRS.Command
{
    public class NewsUpdateCommand
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public string PlayerName { get; set; }
    }
}
