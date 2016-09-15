using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class AuthLogCreateCommandHandler : BaseCommandHandler<AuthLogCreateCommand>
    {
        public AuthLogCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(AuthLogCreateCommand command)
        {
            var authLogRep = this.GetRepository<AuthLog>();
            var playerRep = this.GetRepository<Player>();

            authLogRep.Add(new AuthLog()
            {
                Player = playerRep.GetEntity(command.PlayerName),
                Ip = command.Ip,
                Browser = command.Browser,
                Time = DateTime.Now
            });

            authLogRep.Dispose();
        }
    }
}
