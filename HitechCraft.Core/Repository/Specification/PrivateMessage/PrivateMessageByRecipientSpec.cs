using System;
using System.Linq;
using System.Linq.Expressions;
using HitechCraft.Core.Models.Enum;

namespace HitechCraft.Core.Repository.Specification.PrivateMessage
{
    #region Using Directives

    

    #endregion

    public class PrivateMessageByRecipientSpec : BaseSpecification<Entity.PrivateMessage>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public PrivateMessageByRecipientSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public override Expression<Func<Entity.PrivateMessage, bool>> IsSatisfiedBy()
        {
            return privateMessage => privateMessage.PmPlayerBox.Any(x => x.Player.Name == _playerName && x.PlayerType == PMPlayerType.Recipient);
        }

        #endregion
    }
}
