namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives

    using System.Linq;
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Repository;
    using DAL.Domain;
    using DAL.Repository.Specification;

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
            var playerRep = this._container.Resolve<IRepository<Player>>();
            
            return playerRep
                    .Query(new PlayerByLoginSpec(query.Login))
                    .First();
        }
    }
}
