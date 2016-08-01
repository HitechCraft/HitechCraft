namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using Common.CQRS.Query;
    using Common.Entity;
    using Common.Models.Enum;
    using Common.Projector;
    using HitechCraft.DAL.Domain;

    #endregion

    public class PlayerSkinExistsQuery : IQuery<bool>
    {
        public string UserName { get; set; }
    }
}
