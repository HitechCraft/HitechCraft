using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class RulePointUpdateCommandHandler : BaseCommandHandler<RulePointUpdateCommand>
    {
        public RulePointUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RulePointUpdateCommand command)
        {
            var rulePointRep = GetRepository<RulePoint>();

            var rulePoint = rulePointRep.GetEntity(command.Id);

            rulePoint.Name = command.Name;

            rulePointRep.Update(rulePoint);
            
            rulePointRep.Dispose();
        }
    }
}