namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class PlayerSessionCreateCommand
    {
        public string PlayerName { get; set; }

        public string Server { get; set; }

        public string Session { get; set; }

        public string Token { get; set; }

        public string Md5 { get; set; }
    }
}
