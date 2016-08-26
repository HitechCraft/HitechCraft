﻿namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class RulePointCreateCommandHandler : BaseCommandHandler<RulePointCreateCommand>
    {
        public RulePointCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(RulePointCreateCommand command)
        {
            var rulePointRep = this.GetRepository<RulePoint>();
            
            var rulePoint = new RulePoint()
            {
                Name = command.Name
            };

            rulePointRep.Add(rulePoint);
            
            rulePointRep.Dispose();
        }
    }
}