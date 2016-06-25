namespace WebApplication.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [Bind(Include="Server, Modification")]
    public class ServerModification
    {
        [Key]
        public int Id { get; set; }

        public Server Server { get; set; }

        public Modification Modification { get; set; }
    }
}