namespace HitechCraft.BL.CQRS.Command
{
    public class PlayerSkinCreateOrUpdateCommand
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public byte[] Image { get; set; }
    }
}
