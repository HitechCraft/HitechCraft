using System.Collections.Generic;

namespace HitechCraft.WebAdmin.Models
{
    public class RulePointViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<RuleViewModel> Rules { get; set; }
    }
}