namespace HitechCraft.BL.CQRS.Command
{
    public class AuthLogCreateCommand
    {
        public string PlayerName { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }
    }
}
