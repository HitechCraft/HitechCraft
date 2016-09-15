using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class IKTransactionCreateCommandHandler : BaseCommandHandler<IKTransactionCreateCommand>
    {
        public IKTransactionCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(IKTransactionCreateCommand command)
        {
            var ikTransactionRep = GetRepository<IKTransaction>();
            var playerRep = GetRepository<Player>();

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
