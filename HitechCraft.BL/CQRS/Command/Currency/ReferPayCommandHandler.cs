using System.Linq;
using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.Repository.Specification.Currency;
using HitechCraft.Core.Repository.Specification.Player;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class ReferPayCommandHandler : BaseCommandHandler<ReferPayCommand>
    {
        public ReferPayCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ReferPayCommand command)
        {
            var currencyRep = GetRepository<Currency>();
            var referRep = GetRepository<Referal>();

            var referal = referRep.Query(new ReferalByRefererSpec(command.RefererName));

            if (referal.Any())
            {
                var currency = currencyRep.Query(new CurrencyByPlayerNameSpec(referal.First().Refer.Name)).First();

                var giftAmount = command.RubleAmount*(command.GiftPercents/100f);

                if (giftAmount >= 0.01f)
                {
                    currency.Rubels += giftAmount;

                    currencyRep.Update(currency);
                }
            }
            
            currencyRep.Dispose();
        }
    }
}
