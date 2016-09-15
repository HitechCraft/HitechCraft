namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives
    
    using Core.Models.Json;
    using System.Collections.Generic;

    #endregion

    public class ServerDataListQuery : IQuery<ICollection<JsonMinecraftServerData>>
    {
    }
}
