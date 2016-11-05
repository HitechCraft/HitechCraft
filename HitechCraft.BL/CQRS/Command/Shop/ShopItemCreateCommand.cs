namespace HitechCraft.BL.CQRS.Command
{
    public class ShopItemCreateCommand
    {
        public string GameId { get; set; }

        public string Name { get; set; }
        
        public byte[] Image { get; set; }

        public float Price { get; set; }

        public int ModificationId { get; set; }

        public int CategoryId { get; set; }
    }
}
