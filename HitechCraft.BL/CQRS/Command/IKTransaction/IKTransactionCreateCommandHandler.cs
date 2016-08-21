namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class IKTransactionCreateCommandHandler : BaseCommandHandler<IKTransactionCreateCommand>
    {
        public IKTransactionCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(IKTransactionCreateCommand command)
        {
            var ikTransactionRep = this.GetRepository<IKTransaction>();
            var playerRep = this.GetRepository<Player>();

            var ikTransaction = new IKTransaction()
            {
                TransactionId = command.TransactionId,
                Player = playerRep.GetEntity(command.PlayerName),
                Time = DateTime.Now
            };

            ikTransactionRep.Add(ikTransaction);
            ikTransactionRep.Dispose();
            playerRep.Dispose();
        }
    }
}
