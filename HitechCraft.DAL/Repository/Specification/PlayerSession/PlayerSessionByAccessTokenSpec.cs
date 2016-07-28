﻿namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class PlayerSessionByAccessTokenSpec : BaseSpecification<PlayerSession>
    {
        #region Private Fields

        private readonly string _accessTocken;

        #endregion

        #region Constructor

        public PlayerSessionByAccessTokenSpec(string accessToken)
        {
            this._accessTocken = accessToken;
        }

        #endregion

        #region Expression

        public override Expression<Func<PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Token == this._accessTocken;
        }

        #endregion
    }
}
