namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using Common.CQRS.Query;
    using Common.Models.Json.MinecraftServer;
    using System.Collections.Generic;

    #endregion

    public class ServerDataListQuery : IQuery<ICollection<JsonMinecraftServerData>>
    {
    }
}
