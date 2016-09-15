namespace HitechCraft.BL.CQRS.Command
{
    public class ShopItemBuyCommand
    {
        public string GameId { get; set; }

        public string PlayerName { get; set; }
        
        public int Count { get; set; }
    }
}
