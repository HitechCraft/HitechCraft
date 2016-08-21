namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;
    using System;
    using Common.Models.Enum;

    #endregion

    public class PrivateMessage : BaseEntity<PrivateMessage>
    {
        public virtual string Title { get; set; }

        public virtual string Text { get; set; }
        
        public virtual DateTime TimeCreate { get; set; }
    }
}
