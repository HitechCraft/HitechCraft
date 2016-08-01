namespace HitechCraft.WebApplication.Manager
{
    using NLog;
    using System;

    public static class LogManager
    {
        private static Logger _logger;

        static LogManager()
        {
            _logger = NLog.LogManager.GetLogger("Log");
        }

        /// <summary>
        /// Info logger. Contains success and information loggs
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="prefix">Log prefix</param>
        public static void Info(string message, string prefix = "")
        {
            _logger.Info(ReformatPrefix(prefix) + message);
        }

        /// <summary>
        /// Error logger. Contains scripts errors
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="prefix">Log prefix</param>
        public static void Error(string message, string prefix = "")
        {
            _logger.Error(ReformatPrefix(prefix) + message);
        }

        #region Private Methods

        private static string ReformatPrefix(string prefix = "")
        {
            return (prefix != String.Empty ? "{" + prefix + "} " : "");
        }

        #endregion
    }
}