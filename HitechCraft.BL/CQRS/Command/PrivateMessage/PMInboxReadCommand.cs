namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class PMInboxReadCommand
    {
        public int PMId { get; set; }

        public string PlayerName { get; set; }
    }
}
