namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class RuleUpdateCommand
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
