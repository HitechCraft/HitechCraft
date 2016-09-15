namespace HitechCraft.Core.Models.Json
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Minecraft client data
    /// </summary>
    public class JsonClientData
    {
        #region Properties

        /// <summary>
        /// List of files data object
        /// </summary>
        public ICollection<JsonClientFilesData> FilesData { get; set; }

        /// <summary>
        /// Minecraft client (server) name
        /// </summary>
        public string ClientName { get; set; }

        #endregion
    }
}
