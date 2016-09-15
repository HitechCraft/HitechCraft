namespace HitechCraft.BL.CQRS.Query
{
    public class PlayerSkinExistsQuery : IQuery<bool>
    {
        public string UserName { get; set; }
    }
}
