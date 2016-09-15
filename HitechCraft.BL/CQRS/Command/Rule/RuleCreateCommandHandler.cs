using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class RuleCreateCommandHandler : BaseCommandHandler<RuleCreateCommand>
    {
        public RuleCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RuleCreateCommand command)
        {
            var ruleRep = GetRepository<Rule>();
            var rulePointRep = GetRepository<RulePoint>();
            
            var rule = new Rule()
            {
                Point = rulePointRep.GetEntity(command.PointId),
                Text = command.Text
            };

            ruleRep.Add(rule);

            ruleRep.Dispose();
            rulePointRep.Dispose();
        }
    }
}
