namespace DAL.Domain.Json
{
    /// <summary>
    /// User auth data
    /// </summary>
    public class JsonUserAuthData : JsonStatusData
    {
        #region Properties
        
        /// <summary>
        /// Player session data (online-mode=true)
        /// </summary>
        public JsonSessionData SessionData { get; set; }

        #endregion
    }
}