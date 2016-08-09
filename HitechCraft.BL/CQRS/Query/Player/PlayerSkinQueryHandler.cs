using HitechCraft.Common.Projector;

namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives

    using System.Linq;
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Repository;
    using DAL.Domain;
    using DAL.Repository.Specification;
    using Common.Models.Enum;
    using System;
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
            var playerSkinRep = this._container.Resolve<IRepository<PlayerSkin>>();

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
            catch (System.Exception)
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
