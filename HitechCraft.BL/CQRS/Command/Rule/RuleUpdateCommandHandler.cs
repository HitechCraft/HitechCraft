namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class RuleUpdateCommandHandler : BaseCommandHandler<RuleUpdateCommand>
    {
        public RuleUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RuleUpdateCommand command)
        {
            var ruleRep = this.GetRepository<Rule>();

            var rule = ruleRep.GetEntity(command.Id);

            rule.Text = command.Text;

            ruleRep.Update(rule);
            ruleRep.Dispose();
        }
    }
}
