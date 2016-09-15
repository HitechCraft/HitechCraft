using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class RuleUpdateCommandHandler : BaseCommandHandler<RuleUpdateCommand>
    {
        public RuleUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RuleUpdateCommand command)
        {
            var ruleRep = GetRepository<Rule>();

            var rule = ruleRep.GetEntity(command.Id);

            rule.Text = command.Text;

            ruleRep.Update(rule);
            ruleRep.Dispose();
        }
    }
}
