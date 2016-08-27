namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class NewsImageRemoveCommand
    {
        public int NewsId { get; set; }
    }
}
