namespace HitechCraft.Core.Models.Json
{
    #region Using Direactives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Status of client files
    /// </summary>
    public class JsonClientFilesStatusData : JsonStatusData
    {
        #region Properties

        /// <summary>
        /// Files that must be deleted, loaded or reloaded
        /// </summary>
        public ICollection<JsonErrorFileData> FileData { get; set; }

        #endregion
    }
}