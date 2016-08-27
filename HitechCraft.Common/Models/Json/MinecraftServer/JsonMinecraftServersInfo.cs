namespace HitechCraft.Common.Models.Json.MinecraftServer
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Minecraft server info
    /// </summary>
    public class JsonMinecraftServersInfo
    {
        #region Properties
        
        /// <summary>
        /// Minecraft servers data
        /// </summary>
        public ICollection<JsonMinecraftServerData> ServerData { get; set; }

        /// <summary>
        /// Count of servers
        /// </summary>
        public int ServerCount { get; set; }

        #endregion
    }
}