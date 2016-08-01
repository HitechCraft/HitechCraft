namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class ShopItemBuyCommand
    {
        public string GameId { get; set; }

        public string PlayerName { get; set; }
        
        public int Count { get; set; }
    }
}
