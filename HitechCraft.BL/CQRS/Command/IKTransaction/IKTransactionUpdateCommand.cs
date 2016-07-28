﻿namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class IKTransactionUpdateCommand
    {
        public string TransactionId { get; set; }

        public string NewTransactionId { get; set; }
    }
}
