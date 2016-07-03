namespace DAL.Domain.Json
{
    /// <summary>
    /// Status of some action
    /// </summary>
    public enum JsonStatus
    {
        NO, YES
    }

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