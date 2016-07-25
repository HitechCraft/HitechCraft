namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using Common.CQRS.Query;
    using Common.Entity;
    using Common.Models.Enum;
    using Common.Projector;
    using HitechCraft.DAL.Domain;

    #endregion

    public class PlayerSkinQuery<TResult> : IQuery<TResult>
    {
        public string UserName { get; set; }

        public Gender Gender { get; set; }

        public IProjector<PlayerSkin, TResult> Projector { get; set; }
    }
}
