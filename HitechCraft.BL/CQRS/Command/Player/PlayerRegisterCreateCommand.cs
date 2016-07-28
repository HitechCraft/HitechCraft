namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives
    
    using Common.Models.Enum;

    #endregion

    public class PlayerRegisterCreateCommand
    {
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }
    }
}
