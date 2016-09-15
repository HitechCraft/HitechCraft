namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using System.Linq;
    using HitechCraft.Core.Entity;
    using HitechCraft.Core.Models.Enum;

    #endregion

    public class PrivateMessageRemovedSpec : BaseSpecification<PrivateMessage>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public PrivateMessageRemovedSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public override Expression<Func<PrivateMessage, bool>> IsSatisfiedBy()
        {
            return privateMessage => privateMessage.PmPlayerBox.Any(x => x.Player.Name == _playerName && x.PmType == PMType.Deleted);
        }

        #endregion
    }
}
