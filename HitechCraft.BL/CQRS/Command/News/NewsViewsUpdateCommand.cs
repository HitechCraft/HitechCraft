namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class NewsViewsUpdateCommand
    {
        public int NewsId { get; set; }
    }
}
