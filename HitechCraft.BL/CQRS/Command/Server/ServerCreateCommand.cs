namespace HitechCraft.BL.CQRS.Command
{
    public class ServerCreateCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string ClientVersion { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public int MapPort { get; set; }
    }
}
