using System.Linq;
using HitechCraft.Common.Models.Enum;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;

    #endregion

    public class PMInboxReadCommandHandler : BaseCommandHandler<PMInboxReadCommand>
    {
        public PMInboxReadCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PMInboxReadCommand command)
        {
            var pmRep = GetRepository<PrivateMessage>();
            var pmBoxRep = GetRepository<PMPlayerBox>();

            var pm = pmRep.GetEntity(command.PMId);
            var pmPlayerBoxId = pm.PmPlayerBox.First(x => x.Player.Name == command.PlayerName && x.PmType == PMType.New).Id;

            var pmBox = pmBoxRep.GetEntity(pmPlayerBoxId);
            pmBox.PmType = PMType.Read;

            pmBoxRep.Update(pmBox);
            pmBoxRep.Dispose();
        }
    }
}

