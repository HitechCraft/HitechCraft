﻿namespace DAL.Domain.Json
{
    /// <summary>
    /// Server data
    /// </summary>
    public class JsonMinecraftServerData : JsonMinecraftServerStatusData
    {
        #region Properties

        /// <summary>
        /// Server (client) name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Current count players
        /// </summary>
        public int PlayerCount { get; set; }

        /// <summary>
        /// Max server slots
        /// </summary>
        public int MaxPlayerCount { get; set; }

        #endregion
    }
}