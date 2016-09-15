namespace HitechCraft.BL.CQRS.Command
{
    public class IKTransactionUpdateCommand
    {
        public string TransactionId { get; set; }

        public string NewTransactionId { get; set; }
    }
}
