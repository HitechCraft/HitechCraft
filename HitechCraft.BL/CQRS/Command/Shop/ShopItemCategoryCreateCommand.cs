namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class ShopItemCategoryCreateCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
