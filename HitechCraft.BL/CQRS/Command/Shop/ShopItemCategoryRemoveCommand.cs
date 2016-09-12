namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class ShopItemCategoryRemoveCommand
    {
        public int CategoryId { get; set; }
    }
}
