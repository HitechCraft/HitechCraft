namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class NewsCreateCommand
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public string PlayerName { get; set; }
    }
}
