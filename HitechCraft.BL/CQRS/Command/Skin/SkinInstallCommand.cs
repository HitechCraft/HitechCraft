namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class SkinInstallCommand
    {
        public int SkinId { get; set; }

        public string PlayerName { get; set; }
    }
}
