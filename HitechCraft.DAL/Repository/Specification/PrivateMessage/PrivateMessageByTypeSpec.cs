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

    public class RecipientPrivateMessageByTypeSpec : ISpecification<PrivateMessage>
    {
        #region Private Fields

        private readonly string _playerName;
        private readonly PMType _type;

        #endregion

        #region Constructor

        public RecipientPrivateMessageByTypeSpec(string playerName, PMType type)
        {
            this._playerName = playerName;
            this._type = type;
        }

        #endregion

        #region Expression

        public Expression<Func<PrivateMessage, bool>> IsSatisfiedBy()
        {
            return privateMessage => privateMessage.PmPlayerBox.Any(x => x.Player.Name == _playerName && x.PlayerType == PMPlayerType.Recipient && x.PmType == this._type);
        }

        #endregion
    }
}
