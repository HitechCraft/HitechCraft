namespace HitechCraft.BL.CQRS.Query
{
    public class PlayerByLoginQuery<TResult> : IQuery<TResult>
    {
        public string Login { get; set; }
    }
}
