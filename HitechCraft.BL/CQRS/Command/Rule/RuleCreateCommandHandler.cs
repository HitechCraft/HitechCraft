namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class RuleCreateCommandHandler : BaseCommandHandler<RuleCreateCommand>
    {
        public RuleCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RuleCreateCommand command)
        {
            var ruleRep = this.GetRepository<Rule>();
            var rulePointRep = this.GetRepository<RulePoint>();
            
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
