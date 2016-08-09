namespace HitechCraft.WebApplication.Models
{
    using System.Collections.Generic;

    public class ModificationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }
        
        public string GuideVideoCode { get; set; }

        public byte[] Image { get; set; }

        public IEnumerable<ModificationServerViewModel> Servers { get; set; }
    }
}