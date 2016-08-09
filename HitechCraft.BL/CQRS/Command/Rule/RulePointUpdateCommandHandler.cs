namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class RulePointUpdateCommandHandler : BaseCommandHandler<RulePointUpdateCommand>
    {
        public RulePointUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RulePointUpdateCommand command)
        {
            var rulePointRep = this.GetRepository<RulePoint>();

            var rulePoint = rulePointRep.GetEntity(command.Id);

            rulePoint.Name = command.Name;

            rulePointRep.Update(rulePoint);
            
            rulePointRep.Dispose();
        }
    }
}