namespace HitechCraft.WebApplication.Models
{
    using System.Collections.Generic;

    public class RulePointViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<RuleViewModel> Rules { get; set; }
    }
}