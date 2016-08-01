namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;
    using System.Linq;

    #endregion

    public class RulePointRemoveCommandHandler : BaseCommandHandler<RulePointRemoveCommand>
    {
        public RulePointRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RulePointRemoveCommand command)
        {
            var rulePointRep = this.GetRepository<RulePoint>();

            if(rulePointRep.GetEntity(command.Id).Rules.Any())
                throw new Exception("Пункт правил содержит правила!");

            rulePointRep.Delete(command.Id);

            rulePointRep.Dispose();
        }
    }
}
