using HitechCraft.Core.Databases;
using HitechCraft.Core.Helper;
using HitechCraft.Core.Repository.Specification;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;
    using HitechCraft.BL.CQRS.Command.Base;

    #endregion

    public class DonateGroupIEBuyCommandHandler : BaseCommandHandler<DonateGroupIEBuyCommand>
    {
        public DonateGroupIEBuyCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(DonateGroupIEBuyCommand command)
        {
            var permissionsRep = GetRepository<IEConnection, Permissions>();
            var currencyRep = GetRepository<Currency>();
            var pexInheritanceRep = GetRepository<IEConnection, PexInheritance>();

            var currency = currencyRep.GetEntity(command.CurrencyId);

            #region Check User Pex Info

            if (!permissionsRep.Exist(new PermissionByUserSpec(command.PlayerName)))
                permissionsRep.Add(new Permissions
                {
                    Name = HashHelper.UuidFromString("OfflinePlayer:" + command.PlayerName),
                    Type = 1,
                    Permission = "name",
                    World = "",
                    Value = command.PlayerName
                });

            #endregion

            if (currency.Rubels < command.Price)
                throw new Exception("Недостаточно денег на счете!");

            currency.Rubels -= command.Price;

            command.Permissions.Value = ((int)Math.Round(DateTime.Now.AddMonths(1).Subtract(new DateTime(1970, 1, 1)).TotalSeconds)).ToString();

            command.Permissions.World = "";

            currencyRep.Update(currency);

            permissionsRep.Add(command.Permissions);
            pexInheritanceRep.Add(command.PexInheritance);
            
            currencyRep.Dispose();
            permissionsRep.Dispose();
            pexInheritanceRep.Dispose();
        }
    }
}
