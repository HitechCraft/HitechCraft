using HitechCraft.Core.Models.Enum;

namespace HitechCraft.WebApplication.Models
{
    public class PMPlayerViewModel
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public PMType MessageType { get; set; }

        public PMPlayerType PlayerType { get; set; }
    }
}