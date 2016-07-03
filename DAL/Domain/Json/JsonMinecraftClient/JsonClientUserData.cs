namespace DAL.Domain.Json
{
    /// <summary>
    /// User Minecraft client data
    /// </summary>
    public class JsonClientUserData
    {
        #region Properties
        
        /// <summary>
        /// Unix time now
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// Profile id player uuid)
        /// </summary>
        public string profileId { get; set; }

        /// <summary>
        /// Profile (player) name
        /// </summary>
        public string profileName { get; set; }

        /// <summary>
        /// User textures data
        /// </summary>
        public JsonTextureData textures { get; set; }

        #endregion
    }
}