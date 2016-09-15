using HitechCraft.Core.Repository.Specification.Player;

namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives

    using DAL.Repository;
    using System.Linq;
    using Core.DI;
    using Core.Entity;

    #endregion

    public class PlayerByLoginQueryHandler 
        : IQueryHandler<PlayerByLoginQuery<Player>, Player>
    {
        private readonly IContainer _container;

        public PlayerByLoginQueryHandler(IContainer container)
        {
            _container = container;
        }

        public Player Handle(PlayerByLoginQuery<Player> query)
        {
            var playerRep = _container.Resolve<IRepository<Player>>();
            
            return playerRep
                    .Query(new PlayerByLoginSpec(query.Login))
                    .First();
        }
    }
}
