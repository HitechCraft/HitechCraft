using System;
using System.Linq;
using HitechCraft.Common.Models.Enum;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;

    #endregion

    public class PMInboxRemoveCommandHandler : BaseCommandHandler<PMInboxRemoveCommand>
    {
        public PMInboxRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PMInboxRemoveCommand command)
        {
            var pmRep = GetRepository<PrivateMessage>();
            var pmBoxRep = GetRepository<PMPlayerBox>();

            var pm = pmRep.GetEntity(command.PMId);

            if(pm == null) throw new Exception("Данного сообщения не существует!");

            if (!pm.PmPlayerBox.Any(x => x.Player.Name == command.PlayerName && x.PlayerType == PMPlayerType.Recipient))
                throw new Exception("У вас нет доступа к данному сообщению!");
            
            if (pm.PmPlayerBox.Any(x => x.Player.Name == command.PlayerName && x.PmType == PMType.Deleted))
                throw new Exception("Данное сообщение уже удалено!");

            var pmPlayerBoxId = pm.PmPlayerBox.First(x => x.Player.Name == command.PlayerName && x.PmType != PMType.Deleted).Id;

            var pmBox = pmBoxRep.GetEntity(pmPlayerBoxId);
            pmBox.PmType = PMType.Deleted;

            pmBoxRep.Update(pmBox);
            pmBoxRep.Dispose();
        }
    }
}

