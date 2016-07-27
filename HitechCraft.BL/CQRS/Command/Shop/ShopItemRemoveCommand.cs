namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class ShopItemRemoveCommand
    {
        public string GameId { get; set; }
    }
}
