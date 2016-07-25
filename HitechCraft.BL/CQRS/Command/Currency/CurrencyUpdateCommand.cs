namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using DAL.Domain;

    #endregion

    public class CurrencyUpdateCommand
    {
        public int Id { get; set; }
        
        public double Gonts { get; set; }

        public double Rubles { get; set; }
    }
}
