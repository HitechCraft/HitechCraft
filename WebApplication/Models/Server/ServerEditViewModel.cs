namespace WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ServerEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ClientVersion { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public int Port { get; set; }
    }
}