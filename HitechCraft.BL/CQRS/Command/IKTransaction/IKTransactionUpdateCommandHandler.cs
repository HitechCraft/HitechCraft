namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class IKTransactionUpdateCommandHandler : BaseCommandHandler<IKTransactionUpdateCommand>
    {
        public IKTransactionUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(IKTransactionUpdateCommand command)
        {
            var ikTransactionRep = this.GetRepository<IKTransaction>();

            var ikTransaction = ikTransactionRep.GetEntity(command.TransactionId);
            
            ikTransactionRep.Delete(command.TransactionId);

            ikTransaction.TransactionId = command.NewTransactionId;
            ikTransaction.Time = DateTime.Now;

            ikTransactionRep.Add(ikTransaction);
            ikTransactionRep.Dispose();
        }
    }
}
