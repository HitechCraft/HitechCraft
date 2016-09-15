namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives

    using System.Linq;
    using DAL.Repository.Specification;
    using Core.DI;
    using Core.Entity;
    using DAL.Repository;

    #endregion

    public class PlayerSkinExistsQueryHandler
        : IQueryHandler<PlayerSkinExistsQuery, bool>
    {
        private readonly IContainer _container;

        public PlayerSkinExistsQueryHandler(IContainer container)
        {
            _container = container;
        }

        public bool Handle(PlayerSkinExistsQuery query)
        {
            var playerSkinRep = _container.Resolve<IRepository<PlayerSkin>>();
            
            try
            {
                var playerSkin = playerSkinRep
                    .Query(new PlayerSkinByUserNameSpec(query.UserName))
                    .First();

                playerSkinRep.Dispose();

                if (playerSkin.Image == null) return false;

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
