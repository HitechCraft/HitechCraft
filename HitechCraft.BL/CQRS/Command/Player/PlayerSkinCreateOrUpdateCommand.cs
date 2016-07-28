namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class PlayerSkinCreateOrUpdateCommand
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public byte[] Image { get; set; }
    }
}
