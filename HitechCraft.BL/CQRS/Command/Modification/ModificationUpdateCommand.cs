namespace HitechCraft.BL.CQRS.Command
{
    public class ModificationUpdateCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string GuideVideo { get; set; }

        public string Version { get; set; }
    }
}
