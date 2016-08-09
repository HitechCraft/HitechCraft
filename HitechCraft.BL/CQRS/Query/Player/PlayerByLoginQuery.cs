namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using Common.CQRS.Query;
    using Common.Entity;
    using Common.Projector;
    using HitechCraft.DAL.Domain;

    #endregion

    public class PlayerByLoginQuery<TResult> : IQuery<TResult>
    {
        public string Login { get; set; }
    }
}
