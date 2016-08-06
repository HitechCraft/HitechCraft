namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class SkinCreateCommand
    {
        public string Name { get; set; }

        public byte[] Image { get; set; }
    }
}
