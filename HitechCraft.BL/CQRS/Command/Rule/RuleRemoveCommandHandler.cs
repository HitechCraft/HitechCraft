using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class RuleRemoveCommandHandler : BaseCommandHandler<RuleRemoveCommand>
    {
        public RuleRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RuleRemoveCommand command)
        {
            var ruleRep = GetRepository<Rule>();
            
            ruleRep.Delete(command.Id);

            ruleRep.Dispose();
        }
    }
}
