namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class NewsUpdateCommand
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public string PlayerName { get; set; }
    }
}
