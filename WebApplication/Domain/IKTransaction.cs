namespace WebApplication.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class IKTransaction
    {
        /// <summary>
        /// Object ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Player
        /// </summary>
        public Player Player { get; set; }
        
        /// <summary>
        /// Time of creating transaction
        /// </summary>
        public DateTime Time { get; set; }
    }
}