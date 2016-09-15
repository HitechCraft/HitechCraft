using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.Player
{
    #region Using Directives

    

    #endregion

    public class PlayerByLoginSpec : ISpecification<Entity.Player>
    {
        #region Private Fields

        private readonly string _login;

        #endregion

        #region Constructor

        public PlayerByLoginSpec(string login)
        {
            this._login = login;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.Player, bool>> IsSatisfiedBy()
        {
            return player => player.Name == this._login;
        }

        #endregion
    }
}
