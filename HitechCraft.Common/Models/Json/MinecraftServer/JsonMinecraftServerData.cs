namespace HitechCraft.Common.Models.Json.MinecraftServer
{
    /// <summary>
    /// Server data
    /// </summary>
    public class JsonMinecraftServerData : JsonMinecraftServerStatusData
    {
        #region Properties

        /// <summary>
        /// Server id
        /// </summary>
        public int ServerId { get; set; }

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

        /// <summary>
        /// Server image
        /// </summary>
        public byte[] Image { get; set; }

        #endregion
    }
}