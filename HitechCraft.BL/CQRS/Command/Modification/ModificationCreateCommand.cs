namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class ModificationCreateCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string Version { get; set; }

        public string GuideVideo { get; set; }
    }
}
