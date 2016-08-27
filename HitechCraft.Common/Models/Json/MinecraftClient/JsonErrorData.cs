﻿namespace HitechCraft.Common.Models.Json.MinecraftClient
{
    /// <summary>
    /// Minecraft client error
    /// </summary>
    public class JsonErrorData
    {
        #region Properties
        
        /// <summary>
        /// Error code
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string errorMessage { get; set; }

        #endregion
    }
}