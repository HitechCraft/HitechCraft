namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class PlayerSkinByUserNameSpec : ISpecification<PlayerSkin>
    {
        #region Private Fields

        private readonly string _userName;

        #endregion

        #region Constructor

        public PlayerSkinByUserNameSpec(string userName)
        {
            this._userName = userName;
        }

        #endregion

        #region Expression

        public Expression<Func<PlayerSkin, bool>> IsSatisfiedBy()
        {
            return playerSkin => playerSkin.Player.Name == this._userName;
        }

        #endregion
    }
}
