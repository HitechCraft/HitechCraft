namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class RuleRemoveCommandHandler : BaseCommandHandler<RuleRemoveCommand>
    {
        public RuleRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RuleRemoveCommand command)
        {
            var ruleRep = this.GetRepository<Rule>();
            
            ruleRep.Delete(command.Id);

            ruleRep.Dispose();
        }
    }
}
