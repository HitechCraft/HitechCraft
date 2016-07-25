namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class PlayerSkinRemoveCommand
    {
        public string PlayerName { get; set; }
    }
}
