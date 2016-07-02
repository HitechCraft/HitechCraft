namespace WebApplication.Domain
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Web.Mvc;
    #endregion

    [Bind(Include= "Id, Name, Description, ClientVersion, IpAddress, Port, MapPort, Image, ServerModifications")]
    public class Server
    {
        /// <summary>
        /// Server Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Server name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Server description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Server image
        /// </summary>
        public byte[] Image { get; set; }
        /// <summary>
        /// Client version for cur server
        /// </summary>
        public string ClientVersion { get; set; }
        /// <summary>
        /// Server adress
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Server port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// For Dynmap
        /// </summary>
        public int MapPort { get; set; }

        public ISet<ServerModification> ServerModifications { get; set; }
    }
}