using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class RulePointCreateCommandHandler : BaseCommandHandler<RulePointCreateCommand>
    {
        public RulePointCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RulePointCreateCommand command)
        {
            var rulePointRep = GetRepository<RulePoint>();
            
            var rulePoint = new RulePoint()
            {
                Name = command.Name
            };

            rulePointRep.Add(rulePoint);
            
            rulePointRep.Dispose();
        }
    }
}