namespace HitechCraft.Core.Models.Json
{
    /// <summary>
    /// Minecraft server status
    /// </summary>
    public enum JsonMinecraftServerStatus
    {
        Empty, Full, Online, Offline, Error
    }

    /// <summary>
    /// Minecraft server status data
    /// </summary>
    public class JsonMinecraftServerStatusData
    {
        #region Properties

        /// <summary>
        /// Status
        /// </summary>
        public JsonMinecraftServerStatus Status { get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        public string Message { get; set; }

        #endregion
    }
}