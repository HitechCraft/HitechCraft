namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class IKTransactionCreateCommand
    {
        public string TransactionId { get; set; }

        public string PlayerName { get; set; }
    }
}
