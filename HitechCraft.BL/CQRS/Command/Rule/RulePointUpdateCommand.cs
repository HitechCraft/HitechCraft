namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class RulePointUpdateCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
