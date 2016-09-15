namespace HitechCraft.BL.CQRS.Command
{
    public class ModificationCreateCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string Version { get; set; }

        public string GuideVideo { get; set; }
    }
}
