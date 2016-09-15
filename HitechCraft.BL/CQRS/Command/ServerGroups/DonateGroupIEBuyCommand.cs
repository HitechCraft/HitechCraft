namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.Entity;

    #endregion

    public class DonateGroupIEBuyCommand
    {
        public int CurrencyId { get; set; }

        public string PlayerName { get; set; }

        public int Price { get; set; }

        public Permissions Permissions { get; set; }

        public PexInheritance PexInheritance { get; set; }
    }
}
