namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives
    
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Repository;
    using System.Collections.Generic;
    using Common.Models.Json.MinecraftServer;
    using DAL.Domain;
    using System.Linq;
    using DAL.Domain.Extentions;

    #endregion

    public class ServerDataListQueryHandler
        : IQueryHandler<ServerDataListQuery, ICollection<JsonMinecraftServerData>>
    {
        private readonly IContainer _container;

        public ServerDataListQueryHandler(IContainer container)
        {
            _container = container;
        }

        public ICollection<JsonMinecraftServerData> Handle(ServerDataListQuery query)
        {
            var serverRep = this._container.Resolve<IRepository<Server>>();
            


            return ((IEnumerable<Server>)serverRep.Query()).Select(x => x.GetServerData(x.Image)).ToList();
        }
    }
}
