namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class AuthLogCreateCommand
    {
        public string PlayerName { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }
    }
}
