namespace HitechCraft.Common.Models.Json.MinecraftLauncher
{
    using Enum;

    /// <summary>
    /// Status of some action (can be extended)
    /// </summary>
    public class JsonStatusData
    {
        #region Properties
        
        /// <summary>
        /// Status of action
        /// </summary>
        public JsonStatus Status { get; set; }

        /// <summary>
        /// Action result message
        /// </summary>
        public string Message { get; set; }

        #endregion
    }
}