using HitechCraft.Core.Databases;

namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives
    
    using System.Collections.Generic;
    using System.Linq;
    using Core.DI;
    using Core.Entity;
    using Core.Entity.Extentions;
    using Core.Models.Json;
    using DAL.Repository;
    
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
            var serverRep = _container.Resolve<IRepository<MySQLConnection, Server>>();
            
            return ((IEnumerable<Server>)serverRep.Query()).Select(x => x.GetServerData(x.Image)).ToList();
        }
    }
}
