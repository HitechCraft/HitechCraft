namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class PlayerSessionUpdateCommand
    {
        public int Id { get; set; }

        public string Server { get; set; }

        public string Session { get; set; }

        public string Token { get; set; }
    }
}
