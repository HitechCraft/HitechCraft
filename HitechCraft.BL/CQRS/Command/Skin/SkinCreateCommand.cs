namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.Models.Enum;

    #endregion

    public class SkinCreateCommand
    {
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public Gender Gender { get; set; }
    }
}
