namespace WebApplication.Domain.PermissionsEx
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum TransactionStatus
    {
        Created,
        Viewed
    }

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
        /// IK Transaction status
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Time of creating transaction
        /// </summary>
        public DateTime Time { get; set; }
    }
}