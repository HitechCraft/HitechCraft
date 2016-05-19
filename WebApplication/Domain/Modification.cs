﻿namespace WebApplication.Domain
{
    #region Using Directives
    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class Modification
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public ISet<ServerModification> ServerModifications { get; set; }

    }
}