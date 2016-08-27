namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using DAL.Domain;

    #endregion

    public class CurrencyCreateCommand
    {
        public string PlayerName { get; set; }

        public double Gonts => 100.00;

        public double Rubles => 10.00;
    }
}
