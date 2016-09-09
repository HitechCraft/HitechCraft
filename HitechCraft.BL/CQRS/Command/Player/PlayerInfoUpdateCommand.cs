namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class PlayerInfoUpdateCommand
    {
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public double Gonts { get; set; }

        public double Rubles { get; set; }
    }
}
