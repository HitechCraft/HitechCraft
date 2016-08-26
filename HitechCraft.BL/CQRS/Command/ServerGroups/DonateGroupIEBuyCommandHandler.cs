namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class DonateGroupIEBuyCommandHandler : BaseCommandHandler<DonateGroupIEBuyCommand>
    {
        public DonateGroupIEBuyCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(DonateGroupIEBuyCommand command)
        {
            var permissionsRep = this.GetRepository<Permissions>();
            var currencyRep = this.GetRepository<Currency>();
            var pexInheritanceRep = this.GetRepository<PexInheritance>();

            var currency = currencyRep.GetEntity(command.CurrencyId);

            if(currency.Rubels < command.Price)
                throw new Exception("Недостаточно денег на счете!");

            currency.Rubels -= command.Price;

            command.Permissions.Value = ((int)Math.Round((DateTime.UtcNow.AddDays(30).Subtract(new DateTime(1970, 1, 1))).TotalSeconds)).ToString();

            command.Permissions.World = "*";

            currencyRep.Update(currency);

            permissionsRep.Add(command.Permissions);
            pexInheritanceRep.Add(command.PexInheritance);

            currencyRep.Dispose();
            permissionsRep.Dispose();
            pexInheritanceRep.Dispose();
        }
    }
}
