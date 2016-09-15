namespace HitechCraft.BL.CQRS.Command
{
    public class PMInboxRemoveCommand
    {
        public int PMId { get; set; }

        public string PlayerName { get; set; }
    }
}
