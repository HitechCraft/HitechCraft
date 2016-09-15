namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using HitechCraft.Core.Entity;

    #endregion

    public class CurrencyByPlayerNameQuery : IQuery<Currency>
    {
        public string PlayerName { get; set; }
    }
}
