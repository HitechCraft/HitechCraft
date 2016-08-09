namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class ShopItemCreateCommand
    {
        public string GameId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public float Price { get; set; }

        public int ModificationId { get; set; }

        public int CategoryId { get; set; }
    }
}
