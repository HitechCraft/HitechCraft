namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class RuleCreateCommand
    {
        public string Text { get; set; }

        public int PointId { get; set; }
    }
}
