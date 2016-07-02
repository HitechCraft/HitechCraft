namespace WebApplication.Models
{
    using System.Collections.Generic;

    public class ModificationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public IEnumerable<ServerModificationViewModel> Servers { get; set; }
    }
}