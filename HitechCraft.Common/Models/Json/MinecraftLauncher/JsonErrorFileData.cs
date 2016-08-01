namespace HitechCraft.Common.Models.Json.MinecraftLauncher
{
    /// <summary>
    /// File actions
    /// </summary>
    public enum FileAction
    {
        Load, Reload, Remove
    }

    /// <summary>
    /// Error client files data
    /// </summary>
    public class JsonErrorFileData
    {
        #region Properties
        
        /// <summary>
        /// Client file path
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// File action to solve error
        /// </summary>
        public FileAction FileAction { get; set; }

        #endregion
    }
}