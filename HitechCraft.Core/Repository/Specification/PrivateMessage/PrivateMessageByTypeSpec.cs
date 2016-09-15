using System;
using System.Linq;
using System.Linq.Expressions;
using HitechCraft.Core.Models.Enum;

namespace HitechCraft.Core.Repository.Specification.PrivateMessage
{
    #region Using Directives

    

    #endregion

    public class RecipientPrivateMessageByTypeSpec : ISpecification<Entity.PrivateMessage>
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

        public Expression<Func<Entity.PrivateMessage, bool>> IsSatisfiedBy()
        {
            return privateMessage => privateMessage.PmPlayerBox.Any(x => x.Player.Name == _playerName && x.PlayerType == PMPlayerType.Recipient && x.PmType == this._type);
        }

        #endregion
    }
}
