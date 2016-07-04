namespace DAL.Domain
{
    public class BaseEntity<TEntity> where TEntity : BaseEntity<TEntity>
    {
        /// <summary>
        /// Gets or sets the Identifier.
        /// </summary>
        public virtual int Id { get; set; }
    }
}
