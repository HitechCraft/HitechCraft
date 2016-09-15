using HitechCraft.Core.Repository.Specification.Player;

namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives

    using Core.DI;
    using Core.Entity;
    using Core.Models.Enum;
    using DAL.Repository;
    using System;
    using System.Linq;

    #endregion

    public class PlayerSkinQueryHandler<TResult>
        : IQueryHandler<PlayerSkinQuery<TResult>, TResult>
    {
        private readonly IContainer _container;

        public PlayerSkinQueryHandler(IContainer container)
        {
            _container = container;
        }

        public TResult Handle(PlayerSkinQuery<TResult> query)
        {
            var playerSkinRep = _container.Resolve<IRepository<PlayerSkin>>();

            TResult playerSkin;

            try
            {
                var checkPlayerSkin = playerSkinRep
                    .Query(new PlayerSkinByUserNameSpec(query.UserName))
                    .First();

                if (checkPlayerSkin.Image == null) throw new Exception();

                return playerSkinRep
                    .Query(new PlayerSkinByUserNameSpec(query.UserName), query.Projector)
                    .First();
            }
            catch (Exception)
            {
                switch (query.Gender)
                {
                    case Gender.Male:
                        playerSkin = playerSkinRep
                            .Query(new PlayerSkinByUserNameSpec("DefaultMale"), query.Projector)
                            .First();
                            playerSkinRep.Dispose();
                        break;
                    case Gender.Female:
                        playerSkin = playerSkinRep
                            .Query(new PlayerSkinByUserNameSpec("DefaultFemale"), query.Projector)
                            .First();
                            playerSkinRep.Dispose();
                        break;
                    default:
                        playerSkinRep.Dispose();
                        throw new Exception("Скин не найден");
                }

                return playerSkin;
            }
        }
    }
}
