namespace HitechCraft.BL.CQRS.Command
{
    public class IKTransactionCreateCommand
    {
        public string TransactionId { get; set; }

        public string PlayerName { get; set; }
    }
}
