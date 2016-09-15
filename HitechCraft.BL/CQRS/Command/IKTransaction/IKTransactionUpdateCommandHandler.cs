using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class IKTransactionUpdateCommandHandler : BaseCommandHandler<IKTransactionUpdateCommand>
    {
        public IKTransactionUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(IKTransactionUpdateCommand command)
        {
            var ikTransactionRep = GetRepository<IKTransaction>();

            var ikTransaction = ikTransactionRep.GetEntity(command.TransactionId);
            
            ikTransactionRep.Delete(command.TransactionId);

            ikTransaction.TransactionId = command.NewTransactionId;
            ikTransaction.Time = DateTime.Now;

            ikTransactionRep.Add(ikTransaction);
            ikTransactionRep.Dispose();
        }
    }
}
