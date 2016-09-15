namespace HitechCraft.BL.CQRS.Command
{
    public class PMInboxReadCommand
    {
        public int PMId { get; set; }

        public string PlayerName { get; set; }
    }
}
