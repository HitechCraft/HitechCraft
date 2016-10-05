namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.Models.Enum;
    
    #endregion

    public class PlayerAccountCreateCommand
    {
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }
    }
}
