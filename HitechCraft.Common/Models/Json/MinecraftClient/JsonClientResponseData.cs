namespace HitechCraft.Common.Models.Json.MinecraftClient
{
    /// <summary>
    /// Minecraft client response data
    /// </summary>
    public class JsonClientResponseData
    {
        #region Properties

        /// <summary>
        /// Player uuid
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Response properties array
        /// </summary>
        public JsonClientPropertiesData[] properties { get; set; }

        #endregion
    }
}