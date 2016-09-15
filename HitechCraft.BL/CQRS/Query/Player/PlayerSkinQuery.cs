namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives
    
    using Core.Entity;
    using Core.Models.Enum;

    #endregion

    public class PlayerSkinQuery<TResult> : IQuery<TResult>
    {
        public string UserName { get; set; }

        public Gender Gender { get; set; }

        public IProjector<PlayerSkin, TResult> Projector { get; set; }
    }
}
