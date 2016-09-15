namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using HitechCraft.Core.Entity;

    #endregion

    public class PlayerByLoginSpec : ISpecification<Player>
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

        public Expression<Func<Player, bool>> IsSatisfiedBy()
        {
            return player => player.Name == this._login;
        }

        #endregion
    }
}
