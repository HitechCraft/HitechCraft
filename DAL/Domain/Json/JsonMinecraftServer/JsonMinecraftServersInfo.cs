namespace DAL.Domain.Json
{
    #region Using directives

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
        //TODO: убрать?
        public int ServerCount { get; set; }

        #endregion
    }
}