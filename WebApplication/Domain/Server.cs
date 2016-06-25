namespace WebApplication.Domain
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Web.Mvc;
    #endregion

    [Bind(Include= "Id, Name, Description, ClientVersion, IpAddress, Port, ServerModifications")]
    public class Server
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ClientVersion { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public ISet<ServerModification> ServerModifications { get; set; }
    }
}