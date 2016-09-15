namespace HitechCraft.Core.Models.Json
{
    /// <summary>
    /// File data
    /// </summary>
    public class JsonClientFilesData
    {
        #region Properties

        /// <summary>
        /// Path to file (string format)
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Hash sum (string) of file byte[]
        /// </summary>
        public string HashSum { get; set; }

        /// <summary>
        /// File size (in bytes)
        /// </summary>
        public int FileSize { get; set; }

        #endregion
    }
}
