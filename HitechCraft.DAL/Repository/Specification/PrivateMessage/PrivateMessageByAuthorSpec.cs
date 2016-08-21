using System.Linq;
using System.Text;
using HitechCraft.Common.Models.Enum;
using HitechCraft.DAL.Domain.Extentions;

namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class PrivateMessageByAuthorSpec : ISpecification<PrivateMessage>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public PrivateMessageByAuthorSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public Expression<Func<PrivateMessage, bool>> IsSatisfiedBy()
        {
            return privateMessage => privateMessage.PmPlayerBox.Any(x => x.Player.Name == _playerName && x.PlayerType == PMPlayerType.Author);
        }

        #endregion
    }
}
