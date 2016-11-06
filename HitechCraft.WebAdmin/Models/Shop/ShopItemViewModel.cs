namespace HitechCraft.WebAdmin.Models
{
    public class ShopItemViewModel
    {
        public int Id { get; set; }

        public string GameId { get; set; }
        
        public string Name { get; set; }
        
        public byte[] Image { get; set; }
        
        public float Price { get; set; }

        public int ModificationId { get; set; }

        public string ModificationName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}