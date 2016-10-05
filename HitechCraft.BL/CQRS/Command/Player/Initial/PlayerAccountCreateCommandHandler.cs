namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using Base;
    using Core.Repository.Specification.Player;
    using System;

    #endregion

    public class PlayerAccountCreateCommandHandler : BaseCommandHandler<PlayerAccountCreateCommand>
    {
        public PlayerAccountCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerAccountCreateCommand command)
        {
            var playerRep = GetRepository<Player>();
            var playerInfoRep = GetRepository<PlayerInfo>();

            if (playerRep.Exist(new PlayerByLoginSpec(command.Name)) && playerInfoRep.Exist(new PlayerInfoByEmailSpec(command.Email)))
                throw new Exception("Пользователь или данные уже существуют");

            var playerInfo = new PlayerInfo
            {
                Email = command.Email
            };
            
            playerInfoRep.Add(playerInfo);

            var player = new Player
            {
                Name = command.Name,
                Gender = command.Gender,
                Info = playerInfo
            };

            playerRep.Add(player);

            playerInfoRep.Dispose();
            playerRep.Dispose();
        }
    }
}
