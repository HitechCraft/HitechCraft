using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;

namespace HitechCraft.Core.Repository.Specification.Player
{
    #region Using Directives

    

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
